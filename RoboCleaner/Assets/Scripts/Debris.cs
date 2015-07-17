﻿using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour 
{
	private bool beingPulled = false;
	private GameObject pulledTowards;
	private float pullSpeedFactor = 1.0f;


	void Start () 
	{
	
	}
	
	void Update () 
	{
		if (beingPulled)
		{
			// Move towards player, the closer they are the more quickly
			Vector3 diff = this.transform.position - pulledTowards.transform.position;
			pulledTowards.transform.position += diff / diff.magnitude * pullSpeedFactor;
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "TractorBeam")
		{
			// Entered radius of player, get pulled in
			beingPulled = true;
			pulledTowards = other.gameObject;
		}
		else if (other.tag == "Player")
		{
			// Touchwed player's inner collider, collect points and destroy this!
			Debug.Log("POINTS!");
			GameObject.Destroy(this);
		}
	}
}
