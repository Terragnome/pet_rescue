using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
	protected const float GRAVITY = 9.8f;

	public float walkSpeed = 3f;
	public float turnSpeed = 7f;
	public float dashSpeed = 6f;
	public float dashDuration = 0.3f;

	public float pushForce = 5f;

	protected GameObject avatar {
	    get { return transform.GetChild(0).gameObject; }
	}

	protected float DistanceSquaredTo(Vector3 targetPosition) {
		Vector2 distVector = ToVector2(avatar.transform.position) - ToVector2(targetPosition);
		return distVector.sqrMagnitude;
	}

	protected Vector2 ToVector2(Vector3 v) {
		return new Vector2(v.x, v.z);
	}

	void UpdateMovement () {}

	protected void Start() {}
}