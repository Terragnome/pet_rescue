using UnityEngine;
using System.Collections;

public class BehaviorWander : Behavior
{
    static private float sTurnTimeout = 1.0f;

    private float mTurnTimer;

    public BehaviorWander(Entity entity, float timeout)
        : base(entity, timeout)
    {
        mTurnTimer = 0;
    }    public override string GetDebugName()
    {
        return "Wander";
    }

    public override void Start()
    {
        Vector3 randomForward = GetRandomForward();
        mEntity.transform.forward = randomForward;
    }

    public override void FixedUpdate()
    {
        float delta = Time.deltaTime;
        Transform transform = mEntity.transform;

        mTurnTimer += delta;
        if (mTurnTimer > sTurnTimeout)
        {
            Vector3 randomForward = GetRandomForward();
            transform.forward = randomForward;
            mTurnTimer = 0.0f;
        }

        transform.position += transform.forward * mEntity.walkSpeed * delta;
    }

    private Vector3 GetRandomForward()
    {
        Vector3 randomForward = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.forward;
        return randomForward;
    }
}
