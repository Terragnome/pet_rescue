﻿using UnityEngine;
using System.Collections;

public class Player : Entity {
	float dashTimeLeft = 0f;
	bool isDashing = false;

	protected float liftDistanceSquared;
	protected float liftAngle;
	protected Portable liftTarget = null;

	protected float dropDistanceSquared;

	protected float useDistanceSquared;
	protected float useAngle;
	protected Usable useTarget = null;

	public Vector3 avatarLiftPosition {
		get {
			return (
				avatar.transform.position
				+avatar.transform.forward*2.0f
				+Vector3.up*1.5f
			);
		}
	}

	protected PlayerControls controls {
		get { return GetComponent<PlayerControls>(); }
	}


	protected new void Start () {
		base.Start();
		walkSpeed = 7f;
		turnSpeed = 7f;
		dashSpeed = walkSpeed*3;
		dashDuration = 0.3f;
		pushForce = 5f;

		liftAngle = 60f;
		liftDistanceSquared = Mathf.Pow(4f, 2);

		dropDistanceSquared = liftDistanceSquared;
 		useDistanceSquared = liftDistanceSquared;

 		useAngle = liftAngle;
	}

	void FixedUpdate () {
		float dT = Time.deltaTime;

		UpdateLift(dT);
		UpdateUse(dT);
		UpdateMovement(dT);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
        if(body == null || body.isKinematic) return;

        if(hit.moveDirection.y < -0.3F) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir*pushForce;
	}

	bool IsFacing(Vector3 position, float maxAngle){
		float angle = Vector2.Angle(
			ToVector2(avatar.transform.position) - ToVector2(position),
			ToVector2(avatar.transform.forward*-1)
		);
		return angle <= maxAngle;
	}

	bool IsLifting () {
		return liftTarget != null;
	}

	Usable GetClosestUsable(float useDistance) {
		Usable[] usables = Object.FindObjectsOfType<Usable>();
		Usable closestUsable = null;
		float closestDist = -1;

		foreach(Usable curUsable in usables){
			float usableDist = DistanceSquaredTo(curUsable.transform.position);
			if(
				(closestDist == -1 || usableDist < closestDist)
				&& usableDist <= useDistanceSquared
				&& IsFacing(curUsable.transform.position, useAngle)
			){
				closestUsable = curUsable;
				closestDist = usableDist;
			}
        }
        return closestUsable;
	}

	Portable GetClosestPortable(float liftDistanceSquared) {
		Portable[] portables = Object.FindObjectsOfType<Portable>();
		Portable closestPortable = null;
		float closestPortableDistSquared = liftDistanceSquared;
		foreach(Portable curPortable in portables){
			// Vector3 closestPoint = curPortable.gameObject.GetComponent<Collider>().ClosestPointOnBounds(avatar.transform.position);
			Vector3 closestPoint = curPortable.transform.position;
			float curPortableDistSquared = DistanceSquaredTo(closestPoint);
			if(
				curPortableDistSquared <= closestPortableDistSquared
				&& IsFacing(curPortable.transform.position, liftAngle)
			){
				closestPortable = curPortable;
				closestPortableDistSquared = curPortableDistSquared;
			}
	    }
	    return closestPortable;
	}

	void UpdateLookAt(Vector3 lookVector, float dT, float lookSpeed=1f) {
		avatar.transform.rotation = Quaternion.Lerp(
			avatar.transform.rotation,
			Quaternion.LookRotation(lookVector, Vector3.up),
			lookSpeed*dT
		);
	}

	void UpdateLookAtPosition(Vector3 position, float dT, float lookSpeed=1f) {
		Vector3 lookVector = Vector3.Normalize(position-avatar.transform.position);
		lookVector.y = 0f;
		UpdateLookAt(lookVector, dT, lookSpeed);
	}

	void UpdateUse(float dT) {
		bool checkUse = controls.IsUse();
		if(checkUse){
			if( liftTarget ){
				Tool tool = liftTarget.GetComponent<Tool>() as Tool;
				if( tool ){
					tool.user = this;
					tool.Use();
					return;
				}
			}

			if( useTarget != null ){				
				if(
					DistanceSquaredTo(useTarget.transform.position) < useDistanceSquared
					&& IsFacing(useTarget.transform.position, useAngle)
				){
					useTarget.Use(dT);
					UpdateLookAtPosition(useTarget.transform.position, dT, turnSpeed);
				}else{
					useTarget = null;					
				}
			}

			if( useTarget == null){
				Usable curTarget = GetClosestUsable(useDistanceSquared);
				if(curTarget){ useTarget = curTarget; }
			}
		}else{
			useTarget = null;
		}
	}

	void UpdateLift(float dT) {
		bool checkLift = controls.IsLift();
		if(checkLift){
			if( IsLifting() ){
				liftTarget.Drop();
				liftTarget = null;
			}else{
				Portable curTarget = GetClosestPortable(liftDistanceSquared);
				if(curTarget){
					liftTarget = curTarget.Lift();
				}
			}
		}

		if(liftTarget != null){
			Rigidbody liftRb = liftTarget.GetComponent<Rigidbody>();

			Quaternion rotationA = avatar.transform.rotation * Quaternion.Euler(0, 90, 0);
			float angleA = Quaternion.Angle(liftRb.transform.rotation, rotationA);

			Quaternion rotationB = avatar.transform.rotation * Quaternion.Euler(0, -90, 0);
			float angleB = Quaternion.Angle(liftRb.transform.rotation, rotationB);

			Quaternion targetRotation = angleA < angleB ? rotationA : rotationB;
			liftRb.transform.rotation = targetRotation;

			// TODO: Figure out why this has to be stored in a variable, otherwise Portable position doesn't update
			Vector3 liftRBPosition = liftRb.position;
			liftRb.MovePosition(avatarLiftPosition);
		}
	}

	void UpdateMovement(float dT) {
		float curMoveSpeed = walkSpeed;

		bool isWalkForward = controls.IsForward();
		bool isWalkBack = controls.IsBackward();
		bool isWalkLeft = controls.IsLeft();
		bool isWalkRight = controls.IsRight();

		bool isMoving = isWalkForward || isWalkBack || isWalkLeft || isWalkRight;
		if ( !isMoving ) return;

		bool hasDashed = controls.IsDash();
		if(hasDashed){
			isDashing = true;
			dashTimeLeft = dashDuration;
		}

		if( isDashing ){
			curMoveSpeed = dashSpeed;
			if(dashTimeLeft > 0){
				dashTimeLeft -= dT;
				if(dashTimeLeft <= 0){
					isDashing = false;
					dashTimeLeft = 0f;
				}
			}
		}

		Vector3 moveVector = Vector3.zero;
		if(isWalkBack) moveVector += Vector3.back;
		if(isWalkForward) moveVector += Vector3.forward;
		if(isWalkLeft) moveVector += Vector3.left;
		if(isWalkRight) moveVector += Vector3.right;
		moveVector = Vector3.Normalize(moveVector);

		if(moveVector != Vector3.zero){
			UpdateLookAt(moveVector, dT, turnSpeed);
		}

		Vector3 fullMoveVector = moveVector*curMoveSpeed;
		fullMoveVector += Vector3.down*Entity.GRAVITY;

		GetComponent<CharacterController>().Move(fullMoveVector*dT);
	}
}