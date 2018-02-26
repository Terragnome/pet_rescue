using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {
	public Entity user;

	protected Color debugRayColor = Color.white;

	public float useRate;
	public float rechargeRate;
	public float maxCharge;

	public float charge;
	public float range;

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

	public bool Use() {
		charge = Mathf.Max(0, charge-useRate);
		if(charge > 0){
			Vector3 origin = user.avatar.transform.position;
			Debug.DrawRay(
				origin,
				origin+user.avatar.transform.forward*range,
				debugRayColor
			);
			return true;
		}
		return false;
	}
}