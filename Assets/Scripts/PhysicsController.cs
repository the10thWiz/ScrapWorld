using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class PhysicsController : MonoBehaviour {

	struct RaycastOrigins {
		public Vector2 topRight;
		public Vector2 bottomLeft;
	}

	public struct CollisionInfo {
		public bool above;
		public bool left;
		public bool right;
		public bool below;

		public void Reset() {
			above = false;
			left = false;
			right = false;
			below = false;
		}
	}

	const float skin_depth = 0.015f;
	public int horiz_rays = 3;
	public int vert_rays = 3;
	public LayerMask mask;
	public CollisionInfo collisions;

	float horizSpacing;
	float vertSpacing;
	BoxCollider2D boundingBox;
	RaycastOrigins origins;

	// Use this for initialization
	void Start () {
		boundingBox = GetComponent<BoxCollider2D> ();
	}

	// calculate bounding box corners
	void calcOrigins () {
		Bounds bounds = boundingBox.bounds;
		bounds.Expand (-2 * skin_depth);

		origins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		origins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void calcSpacing () {
		Bounds bounds = boundingBox.bounds;
		bounds.Expand (-2 * skin_depth);

		horiz_rays = Mathf.Max (2, horiz_rays);
		vert_rays = Mathf.Max (2, vert_rays);

		horizSpacing = bounds.size.y / (horiz_rays - 1);
		vertSpacing = bounds.size.x / (vert_rays - 1);
	}

	// Update is called once per frame
	void Update () {
		calcOrigins ();
		calcSpacing ();

		// for (int i = 0; i < vert_rays; i++) {
		// 	Debug.DrawRay (origins.bottomLeft + Vector2.right * vertSpacing * i, Vector2.down * 2, Color.red);
		// 	Debug.DrawRay (origins.topRight + Vector2.left * vertSpacing * i, Vector2.up * 2, Color.red);
		// }
		// for (int i = 0; i < horiz_rays; i++) {
		// 	Debug.DrawRay (origins.topRight + Vector2.down * horizSpacing * i, Vector2.right * 2, Color.red);
		// 	Debug.DrawRay (origins.bottomLeft + Vector2.up * horizSpacing * i, Vector2.left * 2, Color.red);
		// }
	}

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
				
				Debug.DrawRay(origin, Vector2.up * dirY * hit.distance, Color.red);
			}else {
				Debug.DrawRay(origin, Vector2.up * dirY * 2, Color.red);
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
			if (hit) {
				vel.x = (hit.distance - skin_depth) * dirX;
				length = hit.distance;
				collisions.right = dirX == 1;
				collisions.left = dirX == -1;
				Debug.DrawRay(origin, Vector2.right * dirX * hit.distance, Color.red);
			}else {
				Debug.DrawRay(origin, Vector2.right * dirX * 2, Color.red);
			}
		}
	}

	public void Move (Vector3 vel) {
		collisions.Reset();
		HorizontalCollisions (ref vel);
		VerticalCollisions (ref vel);
		transform.Translate (vel);
	}
}