using UnityEngine;using System.Collections;public class BehaviorLevitate : Behavior{    private static float sLevitationForce = 30.0f;    private Rigidbody mRigidbody;    public BehaviorLevitate(Entity entity, float timeout)        : base(entity, timeout)    { }

    public override string GetDebugName()
    {
        return "Levitate";
    }    public override void Start()
    {
        mRigidbody = mEntity.GetComponent<Rigidbody>();
    }    public override void Stop()
    {
        mRigidbody.AddForce(Vector3.up * -sLevitationForce, ForceMode.Impulse);
    }    public override void FixedUpdate()    {        mRigidbody.AddForce(Vector3.up * sLevitationForce);    }}