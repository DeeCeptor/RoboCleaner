using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		// Follow only with x y axes. Not rotation.
		if (transform.gameObject != null)
		{
			this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
		}
	}
}
