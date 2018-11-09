using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Raycaster : MonoBehaviour {
	protected struct RaycastOrigins {
		public Vector2 topRight;
		public Vector2 bottomLeft;
	}

	public struct CollisionInfo {
		public bool above;
		public bool left;
		public bool right;
		public bool below;
		public bool climbSlope;
		public float slopeAngle;
		public float pslopeAngle;

		public void Reset() {
			above = false;
			left = false;
			right = false;
			below = false;
			climbSlope = false;
			pslopeAngle = slopeAngle;
			slopeAngle = 0;
		}
	}
	
	public float skin_depth = 0.015f;
	public int horiz_rays = 3;
	public int vert_rays = 3;
	public LayerMask mask;
	
	protected float horizSpacing;
	protected float vertSpacing;
	protected BoxCollider2D boundingBox;
	protected RaycastOrigins origins;
	// Use this for initialization
	protected virtual void Start () {
		boundingBox = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
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
	
	// calculate bounding box corners
	void calcOrigins () {
		Bounds bounds = boundingBox.bounds;
		bounds.Expand (-2 * skin_depth);

		origins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		origins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
		// Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y), new Vector3(bounds.max.x, bounds.min.y));
		// Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y), new Vector3(bounds.max.x, bounds.max.y));
		// Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y), new Vector3(bounds.min.x, bounds.max.y));
		// Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y), new Vector3(bounds.min.x, bounds.min.y));
	}

	void calcSpacing () {
		Bounds bounds = boundingBox.bounds;
		bounds.Expand (-2 * skin_depth);

		horiz_rays = Mathf.Max (2, horiz_rays);
		vert_rays = Mathf.Max (2, vert_rays);

		horizSpacing = bounds.size.y / (horiz_rays - 1);
		vertSpacing = bounds.size.x / (vert_rays - 1);
	}
}
