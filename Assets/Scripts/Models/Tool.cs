using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {
	public float useRate;
	public float rechargeRate;
	public float maxCharge;

	public float charge;

	protected Portable portable {
		get { return GetComponent<Portable>(); }
	}

	protected Usable usable {
		get { return GetComponent<Usable>(); }
	}

	protected void Start () {
		gameObject.AddComponent<Portable>();
		gameObject.AddComponent<Usable>();

		charge = maxCharge;
	}

	void FixedUpdate () {
		charge = Mathf.Min(maxCharge, charge+rechargeRate);
	}

	public void Use() {
		if(charge > 0){
			charge = Mathf.Max(0, charge-useRate);
			Debug.DrawRay(gameObject.transform.position, gameObject.transform.position+Vector3.forward);
		}
	}
}