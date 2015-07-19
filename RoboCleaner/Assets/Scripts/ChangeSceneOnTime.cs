using UnityEngine;
using System.Collections;

public class ChangeSceneOnTime : MonoBehaviour {
	public float timerToChange = 0;
	public float ChangeTime = 15f;
	// Use this for initialization
	void Start () 
	{
		timerToChange = Time.time + ChangeTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(timerToChange <= Time.time)
		{
			Application.LoadLevel("KevinLevel");
		}
	}
}
