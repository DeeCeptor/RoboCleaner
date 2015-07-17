using UnityEngine;
using System.Collections;

public class BulletTurretScript : MonoBehaviour {
	public GameObject[] enemyList;
	public Transform enemyTarget;
	private float attackTimer = 0;
	public Transform attackType;
	public float range = 100f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(attackTimer <= Time.time){
			attackTimer = Time.time + 0.5f;
			Transform attack = null;
			attack = (Transform)Network.Instantiate (attackType,transform.position, transform.rotation,0);
			BulletScript projectile = attack.gameObject.GetComponent<BulletScript>();
			projectile.target = enemyTarget.transform.position;
		}
		enemyList = GameObject.FindGameObjectWithTag("blue");
		findTarget ();
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
