using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public float speed = 8f;
	public Vector2 target;
	private bool dead = false;
	Vector2 dir = new Vector2(0,0);
	public int layerIgnore = 8;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(dir.magnitude<0.1)
		{
			dir = target - transform.position;
		}
		rigidbody2D.velocity = dir/dir.magnitude * speed;
		if(dead == true)
		{
			Network.Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.gameObject.layer != layerIgnore)
		{
			dead = true;
		}
		
	}
}
