using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : Raycaster {

	public CollisionInfo collisions;
	public float maxClimbAngle = 70;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () { }

	public void VerticalCollisions (ref Vector3 vel) {
		float dirY = Mathf.Sign (vel.y);
		float length = Mathf.Abs (vel.y) + skin_depth;
		for (int i = 0; i < vert_rays; i++) {
			Vector2 origin = (dirY == -1) ? origins.bottomLeft : origins.topRight;
			origin += ((dirY == -1) ? Vector2.right : Vector2.left) * (vertSpacing * i + vel.x);
			RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.up * dirY, length, mask);
			if (hit) {
				vel.y = (hit.distance - skin_depth) * dirY;
				length = hit.distance;
				collisions.above = dirY == 1;
				collisions.below = dirY == -1;

				Debug.DrawRay (origin, Vector2.up * dirY * hit.distance, Color.red);
			} else {
				Debug.DrawRay (origin, Vector2.up * dirY * 2, Color.red);
			}
		}
	}

	public void HorizontalCollisions (ref Vector3 vel) {
		float dirX = Mathf.Sign (vel.x);
		float length = Mathf.Abs (vel.x) + skin_depth;
		for (int i = 0; i < horiz_rays; i++) {
			Vector2 origin = (dirX == -1) ? origins.bottomLeft : origins.topRight;
			origin += ((dirX == -1) ? Vector2.up : Vector2.down) * (horizSpacing * i + vel.y);
			RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.right * dirX, length, mask);
			if(hit.distance == 0) {
				continue;
			}
			if (hit) {
				float angle = Vector2.Angle (hit.normal, Vector2.up);
				if (Mathf.Abs (angle) <= maxClimbAngle) {
					ClimbSlope (ref vel, angle);
				}
				// Maybe only run if not climbing slope?
				vel.x = (hit.distance - skin_depth) * dirX;
				length = hit.distance;
				collisions.right = dirX == 1;
				collisions.left = dirX == -1;
				Debug.DrawRay (origin, Vector2.right * dirX * hit.distance, Color.red);
			} else {
				Debug.DrawRay (origin, Vector2.right * dirX * 2, Color.red);
			}
		}
	}

	void ClimbSlope (ref Vector3 vel, float angle) {
		vel.y = Mathf.Max (Mathf.Sin (angle * Mathf.Deg2Rad) * Mathf.Abs (vel.x), vel.y);
		vel.x *= Mathf.Cos (angle * Mathf.Deg2Rad);
		collisions.below = true;
		collisions.climbSlope = true;
		collisions.slopeAngle = angle;
	}

	public void Move (Vector3 vel, bool standing = false) {
		base.Update ();
		collisions.Reset ();
		collisions.below = standing;
		if (vel.x != 0) {
			HorizontalCollisions (ref vel);
		}
		if (vel.y != 0) {
			VerticalCollisions (ref vel);
		}
		transform.Translate (vel);
	}
}