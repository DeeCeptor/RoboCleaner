using UnityEngine;
using System.Collections;

public class PierceBullet : BulletScript {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.tag == "Wall")
		{
			dead = true;
		}
		
	}
}
