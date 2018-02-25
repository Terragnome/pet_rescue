using UnityEngine;
using System.Collections;

public class ToolSlow : Tool {
	protected void Start () {
		rechargeRate = 1;
		useRate = 2;
		maxCharge = 100;

		range = 10;

		base.Start();
	}
}