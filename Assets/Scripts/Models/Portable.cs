using UnityEngine;
using System.Collections;

public class Portable : MonoBehaviour {
    private BehaviorHeld mBehaviorHeld = null;

    void Start () {
		Rigidbody initRigidbody = rb;
		Collider initCollider = col;
	}

	void Update () {}

	Collider col {
		get {
			SphereCollider sphereCollider = GetComponent<SphereCollider>();
			if(sphereCollider == null){
				sphereCollider = gameObject.AddComponent<SphereCollider>();
				sphereCollider.radius = 0.25f;
			}
			return sphereCollider;
		}
	}

	Rigidbody rb {
		get {
			Rigidbody rigidbody = GetComponent<Rigidbody>();
			if(rigidbody == null){
				rigidbody = gameObject.AddComponent<Rigidbody>();
				rigidbody.angularDrag = 5;
				rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				rigidbody.drag = 5;
				rigidbody.mass = 1;
				rigidbody.useGravity = true;
			}
			return rigidbody;
		}
	}

	public Portable Lift() {
		rb.isKinematic = false;
		rb.useGravity = false;

        BehaviorComponent behaviorComponent = GetComponent<BehaviorComponent>();
        if(behaviorComponent){
	        mBehaviorHeld = new BehaviorHeld(GetComponent<Entity>());
	        behaviorComponent.PushOneShotBehavior(mBehaviorHeld);
        }

		return this;
	}

	public void Drop() {
		if(mBehaviorHeld != null){
			mBehaviorHeld.SetDone();
	        mBehaviorHeld = null;	
		}

        rb.velocity = Vector3.zero;
		rb.useGravity = true;
	}
}
