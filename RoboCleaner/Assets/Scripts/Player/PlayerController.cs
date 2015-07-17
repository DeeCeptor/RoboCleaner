using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float accelerationForce = 10f;
	public float rotationForce = 40000f;
	public float precisionModeFactor = 0.3f;

	private float movementFactor = 1;	// Applied to acceleration. Are we using our full engine force?

	void Start () 
	{
	
	}
	

	void Update () 
	{
		float rotation = -Input.GetAxis("Horizontal");
		float acceleration = Input.GetAxis("Vertical");

		// Are we braking? If so, don't take any input
		if (Input.GetButton("Brake"))
		{

		}
		// Not braking, so take input
		else
		{
			if (Input.GetButton("PrecisionMode"))
			{
				movementFactor = precisionModeFactor;
			}
			else
			{
				movementFactor = 1;
			}
			
			// Ship rotation
			if (rotation != 0) {
				GetComponent<Rigidbody2D>().AddTorque(rotation * rotationForce);
				//GetComponent<Rigidbody2D>().angularVelocity = rotation * rotationForce;
			}
			else {
				GetComponent<Rigidbody2D>().angularVelocity = 0;
			}
			
			// Ship acceleration
			GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration * accelerationForce * movementFactor);
		}
	}
}
