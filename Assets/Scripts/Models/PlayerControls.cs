using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public KeyCode lift;
	public KeyCode use;
	public KeyCode dash;

	public string verticalAxis = "";
	public string horizontalAxis = "";
	public string liftButton = "";
	public string useButton = "";
	public string dashButton = "";

	public bool IsForward() {
		return Input.GetKey(up) || verticalAxis != null && Input.GetAxis(verticalAxis) < 0;
	}

	public bool IsBackward() {
		return Input.GetKey(down) || verticalAxis != null && Input.GetAxis(verticalAxis) > 0;
	}

	public bool IsLeft() {
		return Input.GetKey(left) || horizontalAxis != null && Input.GetAxis(horizontalAxis) < 0;
	}

	public bool IsRight() {
		return Input.GetKey(right) || horizontalAxis != null && Input.GetAxis(horizontalAxis) > 0;
	}

	public bool IsLift() {
		return Input.GetKeyDown(lift) || liftButton != "" && Input.GetButtonDown(liftButton);
	}

	public bool IsUse () {
		return Input.GetKey(use) || useButton != "" && Input.GetButtonDown(useButton);
	}

	public bool IsDash() {
		return Input.GetKeyDown(dash) || dashButton != "" && Input.GetButtonDown(dashButton);
	}
}