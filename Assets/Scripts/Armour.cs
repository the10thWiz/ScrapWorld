using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Armour : MonoBehaviour {

	public float effectiveness = 50;
	public float durablity = 100;

	void Start() {
		GetComponent<Health>().ApplyArmour(effectiveness);
	}

	public void Damage(float damage) {
		durablity-= damage;
	}
	
	public bool IsBroken() {
		return durablity <= 0;
	}
}
