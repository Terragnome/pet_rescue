using UnityEngine;
using System.Collections;

public class BehaviorHeld : Behavior
{
    private bool mDone = false;

    public BehaviorHeld(Entity entity)
        : base(entity)
    { }

    public override string GetDebugName()
    {
        return "Held";
    }

    public override bool IsDone()
    {
        return mDone;
    }

    public override bool IsMovement()
    {
        return true;
    }

    public void SetDone()
    {
        mDone = true;
    }
}
