using UnityEngine;
using System.Collections;

public class ToolPush : Tool {
	protected new void Start () {
		debugRayColor = Color.yellow;

		rechargeRate = 1;
		useRate = 2;
		maxCharge = 100;

		range = 25;

		base.Start();
	}
}