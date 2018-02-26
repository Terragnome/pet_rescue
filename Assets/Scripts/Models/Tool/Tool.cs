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

    private bool mUse = false;

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

    private void Update()
    {
        if (mUse)
        {
            OnUse();
            mUse = false;
        }
    }

    void FixedUpdate () {
		charge = Mathf.Min(maxCharge, charge+rechargeRate);
	}

	public bool Use() {
		charge = Mathf.Max(0, charge-useRate);
		if(charge > 0){
            mUse = true;
            return true;
        }
        return false;
	}

    protected virtual void OnUse()
    { }
}