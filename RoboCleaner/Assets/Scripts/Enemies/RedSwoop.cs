using UnityEngine;
using System.Collections;

public class RedSwoop : RedShip {
	public bool chosen = false;
	public float turnspeed = 0.03f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			if(!chosen){
			chosen = true;
				moveDir = Random.Range(1,3);
			
			
			}
			
			if(health <=0)
			{
			Die();
			for(int i = 0;i < debrisAmount;i++)
			{
				Instantiate (debris, new Vector3(transform.position.x + Random.Range(-2,3),transform.position.y + Random.Range(-2,3	),transform.position.z), transform.rotation);
			}
			
		}
		GetComponent<Rigidbody2D>().velocity = transform.up * speed;
		if(moveDir == 1){
			GetComponent<Rigidbody2D>().AddTorque(-turnspeed);
		}
		if(moveDir == 2){
			GetComponent<Rigidbody2D>().AddTorque(turnspeed);
		}
	}
	
}
