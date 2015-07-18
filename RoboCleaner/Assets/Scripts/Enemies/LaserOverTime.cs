using UnityEngine;

using System.Collections;

public class LaserOverTime : MonoBehaviour {
	public float delay = 4f;
	public float timeToLaser = 0;
	public Transform Laser;
	public bool moved = false;
	// Use this for initialization
	void Start () {
		timeToLaser = Time.time + delay;
		transform.Translate (-transform.right*120,Space.World);
		
		
	}
	
	void Awake()
	{
		//transform.position = transform.position + (transform.right*100);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moved==false)
		{
		moved=true;
		
		}
		if(timeToLaser <= Time.time)
		{
			Instantiate(Laser,transform.position,transform.rotation);
			Destroy(gameObject);
		}
	}
}