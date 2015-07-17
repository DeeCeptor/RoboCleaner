using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}


	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "TractorBeam")
		{

		}
		other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);
	}
}
