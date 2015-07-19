using UnityEngine;
using System.Collections;

public class ChangeSceneOnButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	if(Input.anyKey)
	{
	Application.LoadLevel("Menu");
	}
	
	}
}
