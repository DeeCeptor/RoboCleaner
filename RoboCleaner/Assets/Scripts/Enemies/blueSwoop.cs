using UnityEngine;
using System.Collections;

public class blueSwoop : RedSwoop {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.tag == "redBullet" && otherCollider.gameObject.layer == 13)
		{
			attackScript attack = otherCollider.gameObject.GetComponent<attackScript>();
			health = health - attack.attackDamage;
			Transform debrisMade = (Transform)Instantiate (debris,transform.position, transform.rotation);
			if(otherCollider.gameObject.GetComponent<Rigidbody2D>() != null)
			{
				debrisMade.GetComponent<Rigidbody2D>().velocity = otherCollider.gameObject.GetComponent<Rigidbody2D>().velocity;
			}
		}
		
		if(otherCollider.gameObject.tag == "Player" && otherCollider.gameObject.layer == 14 && ticketed == false)
		{
			ticketed = true;
			int n_score = Scoreboard.board.getModifiedScore(ticketScore);
			StartCoroutine(Scoreboard.board.modifyScore(n_score, Scoreboard.ScoreType.DEBRIS));
			
			GameObject score = Instantiate(Resources.Load("FloatingScore", typeof(GameObject))) as GameObject;
			score.transform.position = this.transform.position;
			score.GetComponent<TextMesh>().text = "+" + n_score + " Ticketed for littering";
		}
		if(otherCollider.gameObject.tag == "Wall" && hitWall == false)
		{
		}
		
	}
}
