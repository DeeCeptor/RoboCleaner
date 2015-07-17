using UnityEngine;
using System.Collections;

public class BlueShip : RedShip{

	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.tag == "redBullet" && otherCollider.gameObject.layer == 13)
		{
			attackScript attack = otherCollider.gameObject.GetComponent<attackScript>();
			health = health - attack.attackDamage;
			Instantiate (debris,transform.position, transform.rotation);
		}
		if(otherCollider.gameObject.tag == "Wall" && hitWall == false)
		{
			if(moveDir == 1){
				moveDir = 2;
			}
			if(moveDir == 2){
				moveDir = 1;
			}
			if(moveDir == 3){
				moveDir = 4;
			}
			if(moveDir == 4){
				moveDir = 3;
			}
			moveTimer = Time.time + 8f;
			hitWall = true;
		}
		
	}

	
}
