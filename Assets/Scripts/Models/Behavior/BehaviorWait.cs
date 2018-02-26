using UnityEngine;using System.Collections;public class BehaviorWait : BehaviorTimed{    public BehaviorWait(Entity entity, float timeout)        : base(entity, timeout)    { }    public override string GetDebugName()    {        return "Wait";    }    public override bool IsMovement()
    {
        return true;
    }}