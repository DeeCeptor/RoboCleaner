using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	private GameObject endingScalingStars;
	private GameObject endingScalingNebula;

	void Start () 
	{
		endingScalingStars = GameObject.Find("Main Camera").transform.FindChild("Very Far Stars").gameObject;
		endingScalingNebula = GameObject.Find("Main Camera").transform.FindChild("Nebula").gameObject;
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
			this.GetComponent<Camera>().orthographicSize = Mathf.Min(75, this.GetComponent<Camera>().orthographicSize);

			endingScalingStars.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * 4;
			endingScalingNebula.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * 4;
		}
	}
}
