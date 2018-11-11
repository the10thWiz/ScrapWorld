using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float max_health = 100;
	private float armour_multiplier = 1;
	private float health;

	void Start () {
		health = max_health;
	}

	// in raw health points
	public void Heal (float increase) {
		health += increase;
		if (health > max_health) {
			health = max_health;
		}
	}

	// armour on range [0, 100], where 0 is no armour, 100 is perfect god tier armour
	public void ApplyArmour (float armour) {
		if (armour == 100) {
			armour_multiplier = 0;
		} else {
			armour_multiplier = 100 / (100 - armour);
		}
	}

	// damage, in raw health points, assuming zero armour. Damage factors armour in to reduce damage
	public void Damage (float damage) {
		Debug.Log(gameObject.name);
		health -= damage * armour_multiplier;
		// add armour damage
		Armour armour = GetComponent<Armour> ();
		if (armour != null) {
			armour.Damage (damage * (1 - armour_multiplier));
		}
	}

	public bool IsDead () {
		return health <= 0;
	}
	
	void Update() {
		if(IsDead()) {
			gameObject.SetActive(false);
		}
	}
}