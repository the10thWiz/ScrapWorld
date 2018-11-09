using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class PlatformController : Raycaster {
	protected struct PassengerMovement {
		public Transform pass;
		public Vector3 vel;
		public bool standing;
		public bool before;

		public PassengerMovement (Transform pass, Vector3 vel, bool standing, bool before) {
			this.pass = pass;
			this.vel = vel;
			this.standing = standing;
			this.before = before;
		}
	}

	protected Vector3 move;
	protected List<PassengerMovement> passengers;
	protected Dictionary<Transform, PhysicsController> passengerDict = new Dictionary<Transform, PhysicsController> ();
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		move = new Vector3 (0, 0.5f, 0);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		passengers = new List<PassengerMovement> ();
		move.y = Mathf.Sin (Time.frameCount / 100) * 2;
		move.x = 0; //Mathf.Sin (Time.frameCount / 100) * 2;
		Vector3 velocity = move * Time.deltaTime;
		calcPassengersVel (velocity);
		MovePassengers (true);
		GetComponent<Transform> ().Translate (velocity);
		MovePassengers (false);
	}

	void calcPassengersVel (Vector3 vel) {
		HashSet<Transform> movedPassengers = new HashSet<Transform> ();

		float dirX = Mathf.Sign (vel.x);
		float dirY = Mathf.Sign (vel.y);
		//vertical
		if (vel.y != 0) {
			float length = Mathf.Abs (vel.y) + skin_depth;
			for (int i = 0; i < vert_rays; i++) {
				Vector2 origin = (dirY == -1) ? origins.bottomLeft : origins.topRight;
				origin += ((dirY == -1) ? Vector2.right : Vector2.left) * (vertSpacing * i);
				RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.up * dirY, length, mask);
				if (hit) {
					if (!movedPassengers.Contains (hit.transform)) {
						movedPassengers.Add (hit.transform);
						float pushX = (dirY == 1) ? vel.x : 0;
						float pushY = vel.y - (hit.distance - skin_depth) * dirY;

						passengers.Add (new PassengerMovement (hit.transform, new Vector3 (pushX, pushY), dirY == 1, true));
					}
				}
			}
		}
		//horizontal
		if (vel.x != 0) {
			float length = Mathf.Abs (vel.x) + skin_depth;
			for (int i = 0; i < horiz_rays; i++) {
				Vector2 origin = (dirX == -1) ? origins.bottomLeft : origins.topRight;
				origin += ((dirX == -1) ? Vector2.up : Vector2.down) * (horizSpacing * i + vel.y);
				RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.right * dirX, length, mask);
				if (hit) {
					if (!movedPassengers.Contains (hit.transform)) {
						movedPassengers.Add (hit.transform);
						float pushY = -skin_depth;
						float pushX = vel.x - (hit.distance - skin_depth) * dirX;

						passengers.Add (new PassengerMovement (hit.transform, new Vector3 (pushX, pushY), false, true));
					}
				}
			}
		}
		//if gravity should keep passenger on platform
		if (vel.y < 0 || (vel.x != 0 && vel.y == 0)) {
			float length = 0.1f + skin_depth;
			for (int i = 0; i < vert_rays; i++) {
				Vector2 origin = origins.topRight;
				origin += Vector2.left * (vertSpacing * i);
				RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.up, length, mask);
				if (hit && !movedPassengers.Contains (hit.transform)) {
					movedPassengers.Add (hit.transform);
					float pushX = vel.x;
					float pushY = vel.y;

					passengers.Add (new PassengerMovement (hit.transform, new Vector3 (pushX, pushY), true, false));
				}
			}
		}
	}

	void MovePassengers (bool before) {
		foreach (PassengerMovement passenger in passengers) {
			if (!passengerDict.ContainsKey (passenger.pass)) {
				passengerDict.Add (passenger.pass, passenger.pass.GetComponent<PhysicsController> ());
			}
			if (passenger.before == before) {
				passengerDict[passenger.pass].Move (passenger.vel, passenger.standing);
			}
		}
	}
}