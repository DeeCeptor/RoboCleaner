﻿using UnityEngine;
using System.Collections;

public class BlueShip : RedShip{


	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.tag == "redBullet" && otherCollider.gameObject.layer == 13)
		{
			attackScript attack = otherCollider.gameObject.GetComponent<attackScript>();
			health = health - attack.attackDamage;
			Transform debrisMade = (Transform)Instantiate (debris,transform.position, transform.rotation);
			if(otherCollider.gameObject.GetComponent<Rigidbody2D>() != null)
			{
				debrisMade.GetComponent<Rigidbody2D>().velocity = otherCollider.gameObject.GetComponent<Rigidbody2D>().velocity;
			}
		}
		if(otherCollider.gameObject.tag == "Wall" && hitWall == false)
		{
		}
		
	}

	
}
