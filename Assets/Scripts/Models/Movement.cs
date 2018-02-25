using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public enum MovementType {Random, Tank};

	protected Entity entity {
		get { return GetComponent<Entity>(); }
	}

	protected virtual void Move () {}

	void FixedUpdate () {
		Move();
	}
}