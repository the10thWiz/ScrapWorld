using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	public float damage = 10;
	public float range = 10;
	public Vector3 position = new Vector3(0, 0, 0);
	protected Transform boundingBox;
	public LayerMask mask;

	void Start() {
		boundingBox = GetComponent<Transform>();
	}

	public override void Attack(Vector3 dir) {
		if(CanAttack()) {
			Attack();
			position.x = Mathf.Abs(position.x) * Mathf.Sign(dir.x);
			dir.Normalize();
			RaycastHit2D hit = Physics2D.Raycast(boundingBox.position+position, dir, range, mask);
			if(hit) {
				Health entity = hit.transform.GetComponent<Health>();
				if(entity){
					entity.Damage(damage);
				}
			}
		}
	}

}
