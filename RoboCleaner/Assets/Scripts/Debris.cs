using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour 
{
	private int debrisScore = 100;
	private bool beingPulled = false;
	private GameObject pulledTowards;
	private float pullSpeedFactor = 1f;
	private float deathTimer;
	public Sprite[] debrisSheet;


	void Start () 
	{
		deathTimer = Time.time + 60;
		this.GetComponent<SpriteRenderer>().sprite = debrisSheet[Random.Range(0,16)];
	}
	
	void Update () 
	{
		if (beingPulled && pulledTowards != null)
		{
			// Move towards player, the closer they are the more quickly
			Vector3 diff = pulledTowards.transform.position - this.transform.position;
			this.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(pulledTowards.transform.position - this.transform.position) * diff.magnitude * 8;
			//this.transform.position -= diff / diff.magnitude * pullSpeedFactor * Time.deltaTime;
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
			// Touched player's inner collider, collect points and destroy this!
			StartCoroutine(Scoreboard.board.modifyScore(debrisScore));

			GameObject score = Instantiate(Resources.Load("FloatingScore", typeof(GameObject))) as GameObject;
			score.transform.position = this.transform.position;
			score.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(pulledTowards.transform.position - this.transform.position) * 2;
			score.GetComponent<TextMesh>().text = "+" + debrisScore;

			GameObject.Destroy(this.gameObject);
		}
		else if (other.tag == "AIZoomba")
		{
			AIZoomba friend = other.gameObject.GetComponent<AIZoomba>();
			friend.debrisGathered = friend.debrisGathered+1;
			
			GameObject.Destroy(this.gameObject);
		}
	}
}
