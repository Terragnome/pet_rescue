using UnityEngine;
using System.Collections;

public class Pet : Entity {
	public enum PetType {Mob, Tank};
	public enum PetName {Dog, Rhino};

	public PetType petType;
	public PetName petName;

	public int petSpeed;
	public int petWeight;

	protected BehaviorComponent behavior {
		get { return GetComponent<BehaviorComponent>(); }
	}

	protected void Start () {
		gameObject.AddComponent<Portable>();
		gameObject.AddComponent<BehaviorComponent>();
		base.Start();
	}
	void Update () {}
}