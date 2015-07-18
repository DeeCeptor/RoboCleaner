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
		if(otherCollider.gameObject.layer != layerIgnore && otherCollider.gameObject.layer != 13)
		{
			Instantiate (explosion, new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.rotation);
			
		}
		
		if(otherCollider.gameObject.tag == "Wall")
		{
			dead = true;
		}
		
	}
}
