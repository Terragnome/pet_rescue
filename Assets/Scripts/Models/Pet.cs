using UnityEngine;
using System.Collections;

public class Pet : Entity {
	public enum PetType {Mob, Tank};
	public enum PetName {Dog};

	public PetType petType;
	public PetName petName;

	public int petSpeed;
	public int petWeight;

	void Start () {}
	void Update () {}
}