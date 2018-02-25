using UnityEngine;
using System.Collections;

public class ToolSlow : Tool {
	protected void Start () {
		cooldown = 1.0f;
		base.Start();
	}
}