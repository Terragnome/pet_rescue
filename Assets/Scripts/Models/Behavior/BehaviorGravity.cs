using UnityEngine;
using System.Collections;

public class BehaviorGravity : BehaviorTimed
{
    const float kAntiGravity = 15.0f;

    private Rigidbody mRigidbody;

    public BehaviorGravity(Entity entity)
        : base(entity, 3f)
    { }

    public override string GetDebugName()
    {
        return "Gravity";
    }

    public override bool IsMovement()
    {
        return true;
    }

    public override void Start()
    {
        base.Start();

        mRigidbody = mEntity.GetComponent<Rigidbody>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        mRigidbody.AddForce(Vector3.up * kAntiGravity);
    }

    public override void Stop()
    {
        base.Stop();

        mRigidbody.AddForce(Vector3.up * -kAntiGravity, ForceMode.Impulse);
    }
}
