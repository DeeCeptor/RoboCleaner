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
		
	}

	
}
