using UnityEngine;
using System.Collections;

public class BehaviorSlowed : BehaviorTimed
{
    private static float sTimeout = 0.5f;
    private static float sSpeedModifier = -1.0f;

    public BehaviorSlowed(Entity entity)
        : base(entity, sTimeout)
    { }

    public override string GetDebugName()
    {
        return "Slowed";
    }

    public override void Start()
    {
        base.Start();

        mEntity.AddSpeedModifier(sSpeedModifier);
    }

    public override void Stop()
    {
        mEntity.RemoveSpeedModifier(sSpeedModifier);
    }
}
