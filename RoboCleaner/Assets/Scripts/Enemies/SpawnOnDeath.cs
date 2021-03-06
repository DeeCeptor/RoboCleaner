﻿using UnityEngine;
using System.Collections;

public class SpawnOnDeath : MonoBehaviour {
	public Transform toSpawn;
	public float timerToDie = 0;
	public float LifeSpan = 4f;
	// Use this for initialization
	void Start () {
		timerToDie = Time.time + LifeSpan;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(timerToDie <= Time.time)
		{
			if(Camera.main.GetComponent<CameraFollow>().target == gameObject.transform)
			{
				Camera.main.GetComponent<CameraFollow>().target = (Transform)Instantiate (toSpawn, new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.rotation);
			}
			else{
			Instantiate (toSpawn, new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.rotation);
			}
			Destroy(gameObject);
			
		}
	}
}
