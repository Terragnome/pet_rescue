using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorComponent : MonoBehaviour
{
    delegate Behavior BehaviorFactory(Entity entity);

    private List<BehaviorFactory> mBasicBehaviors = new List<BehaviorFactory>();
    private int mBasicBehaviorIndex = 0;

    private List<Behavior> mPendingBehaviors = new List<Behavior>();
    private Stack<Behavior> mBehaviorStack = new Stack<Behavior>();

    public void PushOneShotBehavior(Behavior behavior)
    {
        PushBehavior(behavior);
    }

    public void Start()
    {
        mBasicBehaviors.Add(delegate(Entity entity) { return new BehaviorWander(entity, 3.0f); });
        mBasicBehaviors.Add(delegate(Entity entity) { return new BehaviorLevitate(entity, 1.0f); });

        Debug.Assert(mBasicBehaviors.Count > 0);

        Behavior basicBehavior = GetNextBasicBehavior();
        PushBehavior(basicBehavior);
    }

    public void FixedUpdate()
    {
        foreach (Behavior pendingBehavior in mPendingBehaviors)
        {
            PushBehavior(pendingBehavior);
        }

        Behavior activeBehavior = GetActiveBehavior();
        activeBehavior.FixedUpdate();

        activeBehavior.UpdateTimer(Time.deltaTime);
        if (activeBehavior.IsTimedOut())
        {
            PopBehavior();
        }

        if (mBehaviorStack.Count == 0)
        {
            Behavior basicBehavior = GetNextBasicBehavior();
            PushBehavior(basicBehavior);
        }
    }

    private Behavior GetActiveBehavior()
    {
        Debug.Assert(mBehaviorStack.Count > 0);
        return mBehaviorStack.Peek();
    }

    private Behavior GetNextBasicBehavior()
    {
        if (mBasicBehaviorIndex >= mBasicBehaviors.Count)
        {
            mBasicBehaviorIndex = 0;
        }
        Entity entity = GetComponent<Entity>();
        Behavior behavior = mBasicBehaviors[mBasicBehaviorIndex](entity);
        mBasicBehaviorIndex += 1;

        return behavior;
    }

    private void PushBehavior(Behavior behavior)
    {
        if (mBehaviorStack.Count > 0)
        {
            Behavior activeBehavior = mBehaviorStack.Peek();
            activeBehavior.OnSuspend();
            print("Suspended " + activeBehavior.GetDebugName());
        }

        mBehaviorStack.Push(behavior);
        behavior.Start();
        print("Started " + behavior.GetDebugName());
    }

    private void PopBehavior()
    {
        Behavior activeBehavior = mBehaviorStack.Pop();
        activeBehavior.Stop();
        print("Stopped " + activeBehavior.GetDebugName());

        if (mBehaviorStack.Count > 0)
        {
            Behavior behavior = mBehaviorStack.Peek();
            behavior.OnResume();
            print("Resumed " + behavior.GetDebugName());
        }
    }
}
