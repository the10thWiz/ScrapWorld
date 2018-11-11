using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]
public class BasicMeleeAI : MonoBehaviour {

	[SerializeField] Transform target;

	[SerializeField] float maxSpeed;

	[SerializeField] float stoppingDistance;

	[SerializeField] float accelerationRate;

	float timer = 0.1f;
	PhysicsController controller;
	Weapon weapon;
	void Start() {
		controller = GetComponent<PhysicsController>();
		weapon = GetComponent<Weapon>();
	}

	// Update is called once per frame
	void Update () {
		if(Time.time > 1) {
			return;
		}
		Vector2 dir = target.position - transform.position;
		if (dir.magnitude > stoppingDistance) {
			Debug.Log("Moving");
			dir.Normalize();
			controller.Move(dir*Time.deltaTime * (maxSpeed * timer));
		}else {
			Debug.Log("Attacking");
			weapon.Attack(dir);
		}

		timer += Time.deltaTime * accelerationRate;
	}
}