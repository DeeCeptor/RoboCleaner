using UnityEngine;
using System.Collections;

public class WarningStop : MonoBehaviour {
	public float stopTime = 4f;
	public float warpTime = 8f;
	private float stopTimer;
	// Use this for initialization
	void Start () {
	stopTimer = Time.time + stopTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	if(stopTimer<Time.time)
	{
	GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	AIZoomba zoomScript = GetComponent<AIZoomba>();
	GetComponent<AudioSource>().mute = true;
	zoomScript.accelerationForce = 0;
			zoomScript.engineEmitter.GetComponent<ParticleSystem>().Stop();
			zoomScript.smokeEmitter.GetComponent<ParticleSystem>().Stop();
			zoomScript.heatEmitter.GetComponent<ParticleSystem>().Stop();
	}
	
	}
}
