using UnityEngine;
using System.Collections;

public class Pet : Entity {
	public enum PetType {Mob, Tank};
	public enum PetName {Corgi};

	public PetType petType;
	public PetName petName;

	public int petSpeed;
	public int petWeight;
	public Movement petMovement;

	void Start () {}
	void Update () {}
}