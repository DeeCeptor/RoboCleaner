using UnityEngine;
using System.Collections;

public class BlueCorvette : RedCorvette {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
		void FixedUpdate () {
			if(targetTimer <= Time.time)
			{
				targetTimer = targetTimer + 5f;
				enemyList = GameObject.FindGameObjectsWithTag("red");
				findTarget ();
			}
			if(target != null)
			{
				Vector3 dir = target.position - transform.position;
				
				//diff.Normalize();
				
				float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, 0f, rot_z-90);
				GetComponent<Rigidbody2D>().velocity = transform.up * speed;
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
}
