using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public float attack_time = 0.1f;
	
	float lastAttack = -10;
	protected bool CanAttack() {
		return Time.time > lastAttack + attack_time;
	}
	protected void Attack() {
		lastAttack = Time.time;
	}

	public virtual void Attack(Vector3 dir) {
	}

}
