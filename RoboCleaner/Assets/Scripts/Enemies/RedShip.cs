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
	public int ticketScore = 100;
	public bool ticketed = false;
	public int partsCount = 4;
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
		if(otherCollider.gameObject.tag == "Player" && otherCollider.gameObject.layer == 14 && ticketed == false)
		{
		ticketed = true;
			int n_score = Scoreboard.board.getModifiedScore(ticketScore);
			StartCoroutine(Scoreboard.board.modifyScore(n_score, Scoreboard.ScoreType.TICKET));
			
			GameObject score = Instantiate(Resources.Load("FloatingScore", typeof(GameObject))) as GameObject;
			score.transform.position = this.transform.position;
			score.GetComponent<TextMesh>().text = "+" + n_score + " Ticketed for littering";
		}
		if(otherCollider.gameObject.tag == "Wall" && hitWall == false)
		{
		}
		
	}

	public void Die()
	{
		GameObject destroyedShip = this.transform.FindChild("DestroyedShip").gameObject;
		if(destroyedShip!=null)
		{
		destroyedShip.SetActive(true);
		
		// Split off both halves
		GameObject Part1 = destroyedShip.transform.FindChild("Part1").gameObject;
		GameObject Part2 = destroyedShip.transform.FindChild("Part2").gameObject;
		
		Part1.transform.parent = null;
		Part2.transform.parent = null;
		// Send them flying and rotating in randomish directions
		Vector3 cur_velocity = this.GetComponent<Rigidbody2D>().velocity;
		float cur_angular_velocity = 0;
		Part1.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
		Part2.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
		Part1.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
		Part2.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
		
		if(partsCount > 2)
		{
			GameObject Part3 = destroyedShip.transform.FindChild("Part3").gameObject;
			GameObject Part4 = destroyedShip.transform.FindChild("Part4").gameObject;
			
			Part3.transform.parent = null;
			Part4.transform.parent = null;
			Part3.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
			Part4.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
			Part3.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
			Part4.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
		}
		
			if(partsCount > 4)
			{
				GameObject Part5 = destroyedShip.transform.FindChild("Part5").gameObject;
				GameObject Part6 = destroyedShip.transform.FindChild("Part6").gameObject;
				
				Part5.transform.parent = null;
				Part6.transform.parent = null;
				Part5.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
				Part6.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
				Part5.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
				Part6.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
			}
		
		GameObject.Destroy(this.gameObject);
		}
		Destroy(gameObject);
	}

}
