using UnityEngine;
using System.Collections;

public class PetTank : Pet {
	protected new void Start () {
		base.Start();

		petType = PetType.Mob;
		petName = PetName.Rhino;
		petSpeed = 1;
		petWeight = 3;

		walkSpeed = 7f;
	}

	void Update () {}
}