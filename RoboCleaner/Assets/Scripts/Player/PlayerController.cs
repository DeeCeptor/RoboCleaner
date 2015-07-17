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

	private float invulnerability = 0;	// Amount of time we're invulnerable for. If above 0, we're invulnerable


	void Start () 
	{
		colliderArt = this.transform.Find("Collider/ColliderArt").gameObject;
		colliderArt.SetActive(false);
	}
	

	void Update () 
	{
		// Update invulnerability
		if (isInvulnerable())
			invulnerability -= Time.deltaTime;

		float rotation = -Input.GetAxis("Horizontal");
		float acceleration = Input.GetAxis("Vertical");

		// Ship rotation
		if (rotation != 0) {
			//GetComponent<Rigidbody2D>().AddTorque(rotation * rotationForce * movementFactor);
			//GetComponent<Rigidbody2D>().angularVelocity = rotation * rotationForce;
			transform.Rotate (Vector3.forward * rotation * rotationForce * movementFactor);
		}
		else {
			GetComponent<Rigidbody2D>().angularVelocity = 0;
		}


		// Are we braking? If so, don't take any acceleration
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
			
			// Ship acceleration
			if (acceleration != 0)
			{
				// Accelerating, add force
				GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration * accelerationForce * movementFactor);

				// Turn on booster firing, leave trail
			}
		}
	}


	private bool isInvulnerable()
	{
		return (invulnerability > 0);
	}
	public void makeInvulnerable(float duration)
	{
		invulnerability = duration;
	}


	public void TakeHit()
	{
		if (!isInvulnerable())
			Die();	// Hit a bullet, you're dead
	}
	public void Die()
	{
		// Call to revive or game over
		Debug.Log("Died");
		ZoombaSpawner.spawner.PlayerDied();

		GameObject.Destroy(this.gameObject);
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("bullet"))
		{
			TakeHit();	// We've hit a bullet!
		}
	}
}
