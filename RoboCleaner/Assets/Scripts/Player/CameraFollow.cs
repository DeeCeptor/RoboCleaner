using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;

	void Start () 
	{
	
	}
	
	void FixedUpdate () 
	{
		// Follow only with x y axes. Not rotation.
		if (target != null)
		{
			this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
		}
		else if (Scoreboard.board.gameOver)
		{
			this.GetComponent<Camera>().orthographicSize += Time.deltaTime;
			this.GetComponent<Camera>().orthographicSize = Mathf.Min(100, this.GetComponent<Camera>().orthographicSize);
		}
	}
}
