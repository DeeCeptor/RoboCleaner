using UnityEngine;
using System.Collections;

public class RedCorvette : RedShip {
	public Transform target;
	public float targetTimer;
	public GameObject[] enemyList;
	public float turnSpeed = 0.05f;
	
	public float turnTimer = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	if(targetTimer <= Time.time)
	{
		targetTimer = Time.time + 5f;
		enemyList = GameObject.FindGameObjectsWithTag("blue");
		findTarget ();
	}
	if(target != null)
	{
		if(turnTimer<Time.time)
			{
			turnTimer = Time.time+(Time.deltaTime * turnSpeed);
			Vector3 dir = target.position - transform.position;
			
			//diff.Normalize();
			float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z-90), turnSpeed);
			GetComponent<Rigidbody2D>().velocity = transform.up * speed;
		}
	}
			if(health <=0)
			{
			
				Die();
				for(int i = 0;i < debrisAmount;i++)
				{
					Transform debrisMade = (Transform)Instantiate (debris, new Vector3(transform.position.x + Random.Range(-2,2),transform.position.y + Random.Range(-2,2),transform.position.z), transform.rotation);
					debrisMade.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-200,201)/100f , Random.Range(-200,201)/100f );
					
					
				}
		}
	}
	
	public virtual void findTarget()
	{
		if (enemyList.Length == 0)
		{
			enemyList = null;
			return;
		}
		//this enemy attacks the closest player
		//closest player's index
		int targetIndex = 0;
		//lowest distance seen yet
		float curLow = float.PositiveInfinity;
		Vector3 heading;
		// find closest player
		for(int i = 0; i < enemyList.Length; i++)
		{
		if(enemyList[i].name != "redCorvette" && enemyList[i].name != "blueCorvette")
		{
			heading = enemyList[i].transform.position - transform.position;
			if(heading.magnitude < curLow)
			{
				curLow = heading.magnitude;
				targetIndex = i;
			}
		}
		}
		target = enemyList [targetIndex].transform;
		
		
	}
}
