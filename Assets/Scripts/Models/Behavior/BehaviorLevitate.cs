using UnityEngine;using System.Collections;public class BehaviorLevitate : BehaviorTimed{    private static float sLevitationForce = 20.0f;    private Rigidbody mRigidbody;    public BehaviorLevitate(Entity entity, float timeout)        : base(entity, timeout)    { }

    public override string GetDebugName()
    {
        return "Levitate";
    }    public override void Start()
    {
        base.Start();

        mRigidbody = mEntity.GetComponent<Rigidbody>();
    }    public override void Stop()
    {
        base.Stop();

        mRigidbody.AddForce(Vector3.up * -sLevitationForce, ForceMode.Impulse);
    }    public override void FixedUpdate()    {        base.FixedUpdate();        mRigidbody.AddForce(Vector3.up * sLevitationForce);    }}