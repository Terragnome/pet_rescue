using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {
	public double cooldown;

	protected Portable portable {
		get { return GetComponent<Portable>(); }
	}

	protected Usable usable {
		get { return GetComponent<Usable>(); }
	}

	protected void Start () {
		gameObject.AddComponent<Portable>();
		gameObject.AddComponent<Usable>();
	}
}