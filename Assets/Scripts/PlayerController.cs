using System.Collections;
using UnityEngine;

[RequireComponent (typeof (PhysicsController))]
public class PlayerController : MonoBehaviour {

	public float gravity = -20;
	public float moveSpeed = 6;
	public float jumpSpeed = 8;
	public float acceleration = .2f;
	Vector3 velocity;
	Vector3 dir;
	float velocityXSmoothing;

	PhysicsController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PhysicsController> ();
	}

	// Update is called once per frame
	void Update () {
		Move();
		Attack();
	}

	void Move() {
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		float targetX = input.x * moveSpeed;
		if (controller.collisions.below && input.y > 0) {
			velocity.y = input.y * jumpSpeed;
		}
		velocity.x = Mathf.SmoothDamp (velocity.x, targetX, ref velocityXSmoothing, (controller.collisions.below) ? acceleration / 2 : acceleration);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
		dir.y = 0;
		dir.x = Mathf.Sign(velocity.x);
	}

	void Attack() {
		if(Input.GetMouseButton(0)) {
			Weapon w = GetComponent<Weapon>();
			if(w) {
				Debug.Log("Attacking");
				w.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
			}
		}
	}
}