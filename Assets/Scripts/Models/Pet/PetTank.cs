using UnityEngine;
using System.Collections;

public class PetTank : Pet {
	protected new void Start () {
		petType = PetType.Mob;
		petName = PetName.Rhino;
		petSpeed = 1;
		petWeight = 3;

		base.Start();
	}

	void Update () {}
}