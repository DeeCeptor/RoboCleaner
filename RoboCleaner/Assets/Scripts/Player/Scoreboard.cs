using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour 
{
	public static Scoreboard board;

	public int lives = 1;	// How many lives we got. Can't revive if we're out of lives.
	public float time;

	public GameObject livesText;
	public GameObject scoreText;
	public GameObject PauseMenu;
	public GameObject timeText;

	private int score;	// Value >= 0


	void Start () 
	{
		board = this;

		modifyLivesBy(0);
	}


	void Update () 
	{
		if (Input.GetButtonDown("Pause"))
		{
			if (Time.timeScale == 0)
			{
				// Currently paused, unpause game
				Unpause();
			}
			else
			{
				// Currently unpaused, pause game
				Pause();
			}
		}

		if (Time.timeScale != 0)
		{
			// Timer is running if we're not paused
			time += Time.deltaTime;

			int minutes = (int) ((time) / 60.0f);
			int seconds = (int) time;
			int milliseconds = (int) ((time - seconds) * 100);
			timeText.GetComponent<Text>().text = ("" + minutes).PadLeft(2, '0') + ":" + ("" + seconds).PadLeft(2, '0') + "." + ("" + milliseconds).PadLeft(2, '0');
		}
	}


	public void Unpause()
	{
		Time.timeScale = 1;
		PauseMenu.SetActive(false);
	}
	public void Pause()
	{
		Time.timeScale = 0;
		PauseMenu.SetActive(true);
	}


	public void modifyLivesBy(int amount)
	{
		lives += amount;
		livesText.GetComponent<Text>().text = "" + lives;
	}


	public void modifyScore(int amount)
	{
		score += amount;
		score = Mathf.Max(0, score);	// Score can't go below 0
		scoreText.GetComponent<Text>().text = "" + score;
	}


	public void ReturnToMenu()
	{
		Application.LoadLevel("Menu");
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
}
