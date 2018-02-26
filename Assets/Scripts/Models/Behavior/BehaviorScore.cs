using UnityEngine;
using System.Collections;

public class BehaviorScore : Behavior
{
    static private float sTimeout = 3f;
    private static float sLevitationForce = 30.0f;

    private Rigidbody mRigidbody;
    private bool mScored = false;

    public BehaviorScore(Entity entity)
        : base(entity)
    { }

    public override string GetDebugName()
    {
        return "Score";
    }

    public override bool IsDone()
    {
        return false;
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

        Portable portable = mEntity.GetComponent<Portable>();
        if (portable == null || !portable.IsHeld())
        {
            mRigidbody.AddForce(Vector3.up * sLevitationForce, ForceMode.Acceleration);
            if (!mScored)
            {
                mScored = true;

                Score();

                Object.Destroy(mEntity.gameObject, sTimeout);
            }
        }
    }

    private void Score()
    {
        GameObject ui = GameObject.Find("UI");
        if (ui != null)
        {
            UiManager uiManager = ui.GetComponent<UiManager>();
            uiManager.UpdateScore(1);
        }
    }
}
