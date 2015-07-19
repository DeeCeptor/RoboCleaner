using UnityEngine;
using System.Collections;

public class WarningStop : MonoBehaviour {
	public float stopTime = 4f;
	public float warpTime = 8f;
	private float warpTimer = 0f;
	private float stopTimer = 0f;
	public Transform toSpawn;
	public float deadTime = 12f;
	private float deadTimer = 0f;
	private bool warpStart = false;
	// Use this for initialization
	void Start () {
	stopTimer = Time.time + stopTime;
	warpTimer = Time.time + warpTime;
	deadTimer = Time.time + deadTime;
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
	
	if(warpTimer<Time.time && warpStart == false)
		{
		warpStart = true;
		Instantiate (toSpawn, new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.rotation);

		}
	if(deadTimer<Time.time)
		{
			Destroy(gameObject);
		}
	
	}
}
