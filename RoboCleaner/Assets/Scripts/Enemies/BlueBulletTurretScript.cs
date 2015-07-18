using UnityEngine;
using System.Collections;

public class BlueBulletTurretScript : MonoBehaviour {
	public GameObject[] enemyList;
	public Transform enemyTarget;
	private float attackTimer = 0;
	public Transform attackType;
	public float range = 100f;
	public float attackSpeed = 2f;
	public float targetTimer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(attackTimer <= Time.time && enemyList.Length > 0 && enemyTarget!=null){
			if((enemyTarget.transform.position.magnitude - transform.position.magnitude) < range){
				attackTimer = Time.time + attackSpeed + (Random.Range(0,10)/10f);
				Transform attack = null;
				attack = (Transform)Instantiate (attackType,transform.position, transform.rotation);
				BulletScript projectile = attack.gameObject.GetComponent<BulletScript>();
				projectile.target = enemyTarget.transform.position;
				Vector3 dir = enemyTarget.position - transform.position;
				
				//diff.Normalize();
				
				float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				attack.rotation = Quaternion.Euler(0f, 0f, rot_z-180);
			}
		}
		if(targetTimer <= Time.time)
		{
			targetTimer = targetTimer + 5f;
			enemyList = GameObject.FindGameObjectsWithTag("red");
			findTarget ();
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
			heading = enemyList[i].transform.position - transform.position;
			if(heading.magnitude < curLow)
			{
				curLow = heading.magnitude;
				targetIndex = i;
			}
		}
		enemyTarget = enemyList [targetIndex].transform;
		
		
	}
}