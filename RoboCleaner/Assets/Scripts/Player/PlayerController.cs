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
	private GameObject engineEmitter;
	private GameObject smokeEmitter;
	private GameObject brakeEmitter;
	private GameObject heatEmitter;
	
	private AudioSource engineSound;

	private float invulnerability = 0;	// Amount of time we're invulnerable for. If above 0, we're invulnerable


	void Start () 
	{
		colliderArt = this.transform.Find("Collider/ColliderArt").gameObject;
		colliderArt.SetActive(false);
		engineEmitter = this.transform.Find("ShipArt/Thruster System").gameObject;
		smokeEmitter = this.transform.Find("ShipArt/Smoke System").gameObject;
		brakeEmitter = this.transform.Find("ShipArt/Brake System").gameObject;
		heatEmitter = this.transform.Find("ShipArt/Heat System").gameObject;
		
		engineSound = GetComponent<AudioSource>();
	}
	

	void Update () 
	{
		if (Time.timeScale != 0)
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

				// Turn off engine animation
				if(this.engineEmitter.GetComponent<ParticleSystem>().isPlaying) 
				{
					this.engineEmitter.GetComponent<ParticleSystem>().Stop();
					this.smokeEmitter.GetComponent<ParticleSystem>().Stop();
					this.heatEmitter.GetComponent<ParticleSystem>().Stop();
				}
				if(!this.brakeEmitter.GetComponent<ParticleSystem>().isPlaying)
					this.brakeEmitter.GetComponent<ParticleSystem>().Play();
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
					
					engineSound.mute = false;

					// Turn on booster firing, leave trail
					if(!this.engineEmitter.GetComponent<ParticleSystem>().isPlaying) 
					{
						this.engineEmitter.GetComponent<ParticleSystem>().Play();
						this.smokeEmitter.GetComponent<ParticleSystem>().Play();
						this.heatEmitter.GetComponent<ParticleSystem>().Play();
					}
				}
				else
				{
					engineSound.mute = true;
					
					// Turn off engine animation
					if(this.engineEmitter.GetComponent<ParticleSystem>().isPlaying) 
					{
						this.engineEmitter.GetComponent<ParticleSystem>().Stop();
						this.smokeEmitter.GetComponent<ParticleSystem>().Stop();
						this.heatEmitter.GetComponent<ParticleSystem>().Stop();
					}
				}

				if(this.brakeEmitter.GetComponent<ParticleSystem>().isPlaying)
					this.brakeEmitter.GetComponent<ParticleSystem>().Stop();
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


	public void TakeHit(Collider2D aggressor)
	{
		if (!isInvulnerable())
		{
			Die();	// Hit a bullet, you're dead

			if (!Scoreboard.board.died)
			{
				Scoreboard.board.unlockTrophy(35398);
				Scoreboard.board.died = true;
			}
			if (aggressor.gameObject.name.Contains("Laser") && !Scoreboard.board.lazored)
			{
				Scoreboard.board.unlockTrophy(35402);
				Scoreboard.board.lazored = true;
			}
		}
	}
	public void Die()
	{
		// Call to revive or game over
		Debug.Log("Died");

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

		ZoombaSpawner.spawner.PlayerDied();
		
		GameObject.Destroy(this.gameObject);
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("bullet"))
		{
			TakeHit(other);	// We've hit a bullet!
		}
	}
}
