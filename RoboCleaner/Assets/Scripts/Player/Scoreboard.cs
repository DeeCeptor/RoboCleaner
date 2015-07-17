﻿using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour 
{
	public static Scoreboard board;

	public int lives = 1;	// How many lives we got. Can't revive if we're out of lives.
	private int score;	// Value >= 0


	void Start () 
	{
		board = this;
	}


	void Update () 
	{
	
	}


	public void modifyScore(int amount)
	{
		score += amount;
		score = Mathf.Max(0, score);	// Score can't go below 0
	}


	public void submitScore()
	{
		if (GameJolt.API.Manager.Instance.CurrentUser != null)	// Only submit score if we're logged in
			GameJolt.API.Scores.Add(score, "Cleared " + score + " pieces of debris", 0, "", submitScoreCallback);	
	}
	void submitScoreCallback(bool success)
	{
		Debug.Log("Submit Score Callback worked? " + success);
	}


	// Display our score and lives
	void OnGUI() 
	{
		GUI.Label(new Rect(10, 10, 150, 100), "Score " + score);
		GUI.Label(new Rect(10, 30, 150, 100), "Lives " + lives);
	}
}
