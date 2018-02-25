using UnityEngine;
using System.Collections;

public class Pet : MonoBehaviour {
	public enum PetType {Mob, Tank};
	public enum PetName {Corgi};

	public PetType petType;
	public PetName petName;

	public int petSpeed;
	public int petWeight;

	void Start () {}
	void Update () {}
}