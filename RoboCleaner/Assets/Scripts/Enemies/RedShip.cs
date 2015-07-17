using UnityEngine;
using System.Collections;

public class RedShip : MonoBehaviour {
	private int moveDir = 0;
	public float speed = 1f;
	private float moveTimer = 0;
public int health = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moveTimer <= Time.time){
			moveTimer = Time.time + 2f;
			moveDir = Random.Range(0,4);
			}
		if(moveDir == 1){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,speed);
		}
		if(moveDir == 2){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,-speed);
		}
		if(moveDir == 3){
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
		}
		if(moveDir == 4){
			GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,0);
		}
	
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.tag == "blue" && otherCollider.gameObject.layer == 13)
		{
			attackScript attack = otherCollider.gameObject.GetComponent<attackScript>();
			health = health - attack.attackDamage;

		}
		
	}
}
