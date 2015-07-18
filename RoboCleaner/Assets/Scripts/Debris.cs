using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour 
{
	private bool beingPulled = false;
	private GameObject pulledTowards;
	private float pullSpeedFactor = 0.05f;
	private float deathTimer;


	void Start () 
	{
		deathTimer = Time.time + 60;
	}
	
	void Update () 
	{
		if (beingPulled && pulledTowards != null)
		{
			// Move towards player, the closer they are the more quickly
			Vector3 diff = this.transform.position - pulledTowards.transform.position;
			this.transform.position -= diff / diff.magnitude * pullSpeedFactor;
		}
		if(deathTimer<Time.time)
		{
			GameObject.Destroy(this.gameObject);
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "TractorBeam")
		{
			// Entered radius of player, get pulled in
			beingPulled = true;
			pulledTowards = other.gameObject;
		}
		else if (other.tag == "Player")
		{
			// Touchwed player's inner collider, collect points and destroy this!
			Scoreboard.board.modifyScore(1);
			GameObject.Destroy(this.gameObject);
		}
	}
}
