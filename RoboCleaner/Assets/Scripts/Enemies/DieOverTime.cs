using UnityEngine;
using System.Collections;

public class DieOverTime : MonoBehaviour {
public float timerToDie = 0;
public float LifeSpan = 4f;
	// Use this for initialization
	void Start () 
	{
		timerToDie = Time.time + LifeSpan;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(timerToDie <= Time.time && (!this.GetComponent<Renderer>().isVisible || (Scoreboard.board != null && Scoreboard.board.gameOver)))
		{
			Destroy(gameObject);
		}
	}
}
