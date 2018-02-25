using UnityEngine;
using System.Collections;

public class PetMob : Pet {
	protected void Start () {
		petType = PetType.Mob;
		petSpeed = 1;
		petWeight = 1;

		base.Start();
	}

	void Update () {}
}