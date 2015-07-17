using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float accelerationForce = 10f;
	public float rotationForce = 1f;
	public float precisionModeFactor = 0.3f;
	public float brakeFactor = 0.95f;

	private float movementFactor = 1;	// Applied to acceleration and rotation. Are we using our full engine force?
	private GameObject colliderArt;

	void Start () 
	{
		colliderArt = this.transform.Find("Collider/ColliderArt").gameObject;
		colliderArt.SetActive(false);
	}
	

	void Update () 
	{
		float rotation = -Input.GetAxis("Horizontal");
		float acceleration = Input.GetAxis("Vertical");

		// Are we braking? If so, don't take any input
		if (Input.GetButton("Brake"))
		{
			GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * brakeFactor;
		}
		// Not braking, so take input
		else
		{
			if (Input.GetButton("PrecisionMode"))
			{
				colliderArt.SetActive(true);
				movementFactor = precisionModeFactor;
			}
			else
			{
				colliderArt.SetActive(false);
				movementFactor = 1;
			}
			
			// Ship rotation
			if (rotation != 0) {
				//GetComponent<Rigidbody2D>().AddTorque(rotation * rotationForce * movementFactor);
				//GetComponent<Rigidbody2D>().angularVelocity = rotation * rotationForce;
				transform.Rotate (Vector3.forward * rotation * rotationForce * movementFactor);
			}
			else {
				GetComponent<Rigidbody2D>().angularVelocity = 0;
			}
			
			// Ship acceleration
			if (acceleration != 0)
			{
				// Accelerating, add force
				GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration * accelerationForce * movementFactor);

				// Turn on booster firing, leave trail
			}
		}
	}


	public void Die()
	{

	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("bullet"))
		{
			Die();	// Hit a bullet, you're dead
		}
	}
}
