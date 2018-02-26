using UnityEngine;
using System.Collections;

public class ToolSlow : Tool {
	protected void Start () {
		debugRayColor = Color.blue;

		rechargeRate = 1;
		useRate = 2;
		maxCharge = 100;

		range = 25;

		base.Start();
	}
}