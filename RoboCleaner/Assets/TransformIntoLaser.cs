using UnityEngine;
using System.Collections;

public class LaserOverTime : MonoBehaviour {
public float delay = 4f;
public float timeToLaser = 0;
public Transform Laser;
	// Use this for initialization
	void Start () {
	timeToLaser = Time.time + delay;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	if(timeToLaser <= Time.time)
	{
	Instantiate(Laser,transform.position,transform.rotation);
	Destroy(gameObject);
	}
	}
}
