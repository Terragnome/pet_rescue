using UnityEngine;
using System.Collections;

public class PetMob : Pet {
	protected new void Start () {
		base.Start();

		petType = PetType.Mob;
		petName = PetName.Dog;
		petSpeed = 1;
		petWeight = 1;

		walkSpeed = 7f;
	}

	void Update () {}
}