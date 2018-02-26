using UnityEngine;
using System.Collections;

public abstract class BehaviorTimed : Behavior
{
    private float mTimeout;    private float mTimer;    public BehaviorTimed(Entity entity, float timeout)
        : base(entity)
    {
        mTimeout = timeout;        mTimer = 0;
    }

    public override bool IsDone()
    {
        return mTimer > mTimeout;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        mTimer += Time.deltaTime;
    }
}
