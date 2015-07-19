using UnityEngine;
using System.Collections;

public class AIZoomba : MonoBehaviour {

	public float accelerationForce = 1f;
	public int debrisGathered = 0;
	public float turnSpeed = 2f;
	private float movementFactor = 1;	// Applied to acceleration and rotation. Are we using our full engine force?
	public GameObject colliderArt;
	public GameObject engineEmitter;
	public GameObject smokeEmitter;
	public GameObject brakeEmitter;
	public GameObject heatEmitter;
	public Transform target;
	public GameObject[] debrisList;
	public Transform debris;
	public float turnTimer = 0f;
	// Use this for initialization
	void Start () 
	{
		colliderArt = this.transform.Find("Collider/ColliderArt").gameObject;
		colliderArt.SetActive(false);
		engineEmitter = this.transform.Find("ShipArt/Thruster System").gameObject;
		smokeEmitter = this.transform.Find("ShipArt/Smoke System").gameObject;
		brakeEmitter = this.transform.Find("ShipArt/Brake System").gameObject;
		heatEmitter = this.transform.Find("ShipArt/Heat System").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target == null)
		{
			debrisList = GameObject.FindGameObjectsWithTag("Debris");
			findTarget ();
			
			
		}
		if(turnTimer<Time.time)
		{
			turnTimer = Time.time+(Time.deltaTime * turnSpeed);
			Vector3 dir = target.position - transform.position;
		
		//diff.Normalize();
			float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z-90), turnSpeed);
			GetComponent<Rigidbody2D>().velocity = transform.up * accelerationForce;
		}
		
	}
	
	public void Die()
	{
		
		// Split ship in half
		GameObject destroyedShip = this.transform.FindChild("ShipArt/DestroyedShip").gameObject;
		destroyedShip.SetActive(true);
		// Split off both halves
		GameObject Half1 = destroyedShip.transform.FindChild("Half1").gameObject;
		GameObject Half2 = destroyedShip.transform.FindChild("Half2").gameObject;
		Half1.transform.parent = null;
		Half2.transform.parent = null;
		// Send them flying and rotating in randomish directions
		Vector3 cur_velocity = this.GetComponent<Rigidbody2D>().velocity;
		float cur_angular_velocity = this.GetComponent<Rigidbody2D>().angularVelocity;
		Half1.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
		Half2.GetComponent<Rigidbody2D>().velocity = cur_velocity + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
		Half1.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
		Half2.GetComponent<Rigidbody2D>().angularVelocity = cur_angular_velocity + Random.Range(-25, 25);
		
		for(int i = 0;i < debrisGathered;i++)
		{
			Transform debrisMade = (Transform)Instantiate (debris, new Vector3(transform.position.x + Random.Range(-2,2),transform.position.y + Random.Range(-2,2),transform.position.z), transform.rotation);
			debrisMade.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-200,201)/100f , Random.Range(-200,201)/100f );
			Instantiate (debris, new Vector3(transform.position.x + (Random.Range(-200,201)/100f),transform.position.y + (Random.Range(-200,201)/100f) ,transform.position.z), transform.rotation);
		}
		
		GameObject.Destroy(this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("bullet"))
		{
			Die();	// We've hit a bullet!
		}
		}
		
	public virtual void findTarget()
	{
		if (debrisList.Length == 0)
		{
			debrisList = null;
			return;
		}
		//the Zoomba follows debris
		//closest debris index
		int targetIndex = 0;
		//lowest distance seen yet
		float curLow = float.PositiveInfinity;
		Vector3 heading;
		// find closest debri
		for(int i = 0; i < debrisList.Length; i++)
		{
				heading = debrisList[i].transform.position - transform.position;
				if(heading.magnitude < curLow)
				{
					curLow = heading.magnitude;
					targetIndex = i;
				}
		}
		target = debrisList [targetIndex].transform;
		
		
	}
}
