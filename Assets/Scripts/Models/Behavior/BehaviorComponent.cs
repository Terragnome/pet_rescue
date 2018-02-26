using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorComponent : MonoBehaviour
{
    delegate Behavior BehaviorFactory(Entity entity);

    private List<BehaviorFactory> mBasicBehaviors = new List<BehaviorFactory>();
    private int mBasicBehaviorIndex = 0;

    private List<Behavior> mPendingBehaviors = new List<Behavior>();
    private List<Behavior> mBehaviors = new List<Behavior>();

    private Behavior mPendingMovementBehavior = null;
    private Behavior mMovementBehavior = null;

    public void PushOneShotBehavior(Behavior behavior)
    {
        PushBehavior(behavior);
    }

    public void Start()
    {
        mBasicBehaviors.Add(delegate(Entity entity) { return new BehaviorWander(entity, 3.0f); });
        //mBasicBehaviors.Add(delegate(Entity entity) { return new BehaviorLevitate(entity, 1.0f); });
    }

    public void FixedUpdate()
    {
        // Clean up completed behaviors
        for (int i = mBehaviors.Count - 1; i >= 0; --i)
        {
            Behavior behavior = mBehaviors[i];
            if (behavior.IsDone())
            {
                behavior.Stop();
                mBehaviors.RemoveAt(i);
            }
        }

        // Update up movement if necessary
        if (mMovementBehavior != null)
        {
            if (mPendingMovementBehavior != null || mMovementBehavior.IsDone())
            {
                mMovementBehavior.Stop();
                mMovementBehavior = null;
            }
        }
        if (mMovementBehavior == null)
        {
            mMovementBehavior = mPendingMovementBehavior ?? GetNextBasicBehavior();
            mMovementBehavior.Start();
            mPendingMovementBehavior = null;
        }

        // Start pending behaviors
        foreach (Behavior behavior in mPendingBehaviors)
        {
            behavior.Start();
            mBehaviors.Add(behavior);
        }
        mPendingBehaviors.Clear();

        // Update all behaviors
        mMovementBehavior.FixedUpdate();
        foreach (Behavior behavior in mBehaviors)
        {
            behavior.FixedUpdate();
        }
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
        if (behavior.IsMovement())
        {
            // Only take the latest requested movement behavior
            mPendingMovementBehavior = behavior;
        }
        else
        {
            mPendingBehaviors.Add(behavior);
        }
    }
}
