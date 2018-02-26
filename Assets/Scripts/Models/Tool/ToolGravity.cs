using UnityEngine;
using System.Collections;

public class ToolGravity : Tool {
	protected new void Start () {
		debugRayColor = Color.green;

		rechargeRate = 1;
		useRate = 2;
		maxCharge = 100;

		range = 25;

		base.Start();
	}

    protected override void OnUse()
    {
        Transform tranform = user.avatar.transform;
        Vector3 origin = transform.position;

        float beamSize = 1.0f;

        RaycastHit hit;
        if (Physics.SphereCast(origin, beamSize, tranform.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                BehaviorComponent behaviorComponent = hit.rigidbody.GetComponent<BehaviorComponent>();
                if (behaviorComponent != null)
                {
                    Entity entity = behaviorComponent.GetComponent<Entity>();
                    if (entity != null)
                    {
                        behaviorComponent.PushOneShotBehavior(new BehaviorGravity(entity));
                    }
                }
            }
        }

        Debug.DrawRay(
            origin,
            origin + user.avatar.transform.forward * range,
            debugRayColor
        );
    }
}