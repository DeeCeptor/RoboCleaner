using UnityEngine;
using System.Collections;

public class RedShip : MonoBehaviour {
	public int moveDir = 0;
	public float speed = 1f;
	public float moveTimer = 0f;
	public int debrisAmount = 5;
	public Transform debris;
	public bool hitWall = false;
	public int health = 100;
	public Transform explosion;
	public int rotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moveTimer <= Time.time){
		hitWall = false;
			moveTimer = Time.time + Random.Range(2,8);
			moveDir = Random.Range(0,3);
			rotate = Random.Range(0,3);
			if(rotate == 1){
				GetComponent<Rigidbody2D>().AddTorque(15);
			}
			if(rotate == 2){
				GetComponent<Rigidbody2D>().AddTorque(-15);
			}
			}
		if(moveDir == 1){
			GetComponent<Rigidbody2D>().velocity = transform.up * speed;
		}
		if(moveDir == 2){
			GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
		}
		
		
		
	if(health <=0)
		{
			Die();
			for(int i = 0;i < debrisAmount;i++)
			{
				Transform debrisMade = (Transform)Instantiate (debris, new Vector3(transform.position.x + Random.Range(-2,2),transform.position.y + Random.Range(-2,2),transform.position.z), transform.rotation);
				debrisMade.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-200,201)/100f , Random.Range(-200,201)/100f );
				Instantiate (debris, new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.rotation);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.tag == "blueBullet" && otherCollider.gameObject.layer == 13)
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

	public void Die()
	{
		Destroy(gameObject);
	}

}
