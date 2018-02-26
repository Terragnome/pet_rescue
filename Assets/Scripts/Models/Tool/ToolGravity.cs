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
}