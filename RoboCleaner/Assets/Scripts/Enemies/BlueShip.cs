using UnityEngine;
using System.Collections;

public class BlueShip : RedShip{


	
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
			StartCoroutine(Scoreboard.board.modifyScore(ticketScore));
			ticketed = true;
			GameObject score = Instantiate(Resources.Load("FloatingScore", typeof(GameObject))) as GameObject;
			score.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z -5);
			score.GetComponent<TextMesh>().text = "fined " + ticketScore + " for littering";
		}
		if(otherCollider.gameObject.tag == "Wall" && hitWall == false)
		{
		}
		
	}

	
}
