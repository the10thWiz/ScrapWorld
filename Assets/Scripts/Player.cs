﻿using System.Collections;
using UnityEngine;

[RequireComponent (typeof (PhysicsController))]
public class Player : MonoBehaviour {

	public float gravity = -20;
	public float moveSpeed = 6;
	public float jumpSpeed = 8;
	Vector3 velocity;

	PhysicsController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PhysicsController> ();
	}

	// Update is called once per frame
	void Update () {
		if(controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		velocity.x = input.x * moveSpeed;
		if(controller.collisions.below && input.y > 0) {
			velocity.y = input.y * jumpSpeed;
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
}