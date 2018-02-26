using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
	protected const float GRAVITY = 9.8f;

	public float walkSpeed;
	public float turnSpeed;
	public float dashSpeed;
	public float dashDuration;

	public float pushForce;

	public GameObject avatar {
	    get { return transform.GetChild(0).gameObject; }
	}

	protected float DistanceSquaredTo(Vector3 targetPosition) {
		Vector2 distVector = ToVector2(avatar.transform.position) - ToVector2(targetPosition);
		return distVector.sqrMagnitude;
	}

	protected Vector2 ToVector2(Vector3 v) {
		return new Vector2(v.x, v.z);
	}

    private List<float> mSpeedModifiers = new List<float>();

	void UpdateMovement () {}

	protected void Start() {
        walkSpeed = 3f;
        turnSpeed = 7f;
        dashSpeed = 5f;
        dashDuration = 0.3f;

        pushForce = 5f;
    }

    public float GetModifiedSpeed(float speed)
    {
        float modifier = GetSpeedModifier();
        float modifiedSpeed = speed + modifier;
        if (modifiedSpeed > 0.0f)
        {
            return modifiedSpeed;
        }
        return 0.0f;
    }

    public float GetSpeedModifier()
    {
        float totalModifier = 0.0f;
        foreach (float modifier in mSpeedModifiers)
        {
            totalModifier += modifier;
        }
        return totalModifier;
    }

    public void AddSpeedModifier(float speedModifier)
    {
        mSpeedModifiers.Add(speedModifier);
    }

    public void RemoveSpeedModifier(float speedModifier)
    {
        mSpeedModifiers.Remove(speedModifier);
    }
}