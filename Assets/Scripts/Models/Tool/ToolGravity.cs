using UnityEngine;
using System.Collections;

public class ToolGravity : Tool {
	protected void Start () {
		debugRayColor = Color.white;

		rechargeRate = 1;
		useRate = 1;
		maxCharge = 3;

		range = 2;

		base.Start();
	}
}