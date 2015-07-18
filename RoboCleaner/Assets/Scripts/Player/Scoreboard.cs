using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour 
{
	public static Scoreboard board;

	public int lives = 1;	// How many lives we got. Can't revive if we're out of lives.
	public float time;
	public int debrisGotten = 0;

	public GameObject livesText;
	public GameObject scoreText;
	public GameObject PauseMenu;
	public GameObject timeText;

	public bool gameOver = false;

	private int score;	// Value >= 0

	// TROPHIES
	bool helper, janitor, cleaner, MrClean = false;	// Debris trophies
	bool greenShirt, insurable, survivor = false;	// Time survival trophies
	public bool died, lazored = false;	// Die once, Die from lazor beam


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

		if (Time.timeScale != 0 && !gameOver)
		{
			// Timer is running if we're not paused
			time += Time.deltaTime;

			int minutes = (int) ((time) / 60.0f);
			int seconds = (int) time;
			int milliseconds = (int) ((time - seconds) * 100);
			timeText.GetComponent<Text>().text = ("" + minutes).PadLeft(2, '0') + ":" + ("" + seconds).PadLeft(2, '0') + "." + ("" + milliseconds).PadLeft(2, '0');
		
			if (minutes > 4 && !greenShirt)
			{
				unlockTrophy(35397);
				greenShirt = true;
			}
			else if (minutes > 9 && !insurable)
			{
				unlockTrophy(35405);
				insurable = true;
			}
			else if (minutes > 29 && !survivor)
			{
				unlockTrophy(35406);
				survivor = true;
			}
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
		debrisGotten++;

		if (debrisGotten > 0 && !helper)
		{
			unlockTrophy(35399);
			helper = true;
		}
		else if (debrisGotten > 49 && !janitor)
		{
			unlockTrophy(35401);
			janitor = true;
		}
		else if (debrisGotten > 49 && !cleaner)
		{
			unlockTrophy(35403);
			cleaner = true;
		}
		else if (debrisGotten > 1000 && !MrClean)
		{
			unlockTrophy(35404);
			MrClean = true;
		}
	}


	public void unlockTrophy(int trophyID)
	{
		if (GameJolt.API.Manager.Instance != null && GameJolt.API.Manager.Instance.CurrentUser != null)	// Only submit score if we're logged in
			GameJolt.API.Trophies.Unlock(trophyID, trophyCallback);
	}
	public void trophyCallback(bool success)
	{
		Debug.Log("Got trophy: " + success);
	}


	public void submitScore()
	{
		if (GameJolt.API.Manager.Instance != null && GameJolt.API.Manager.Instance.CurrentUser != null)	// Only submit score if we're logged in
			GameJolt.API.Scores.Add(score, "Kilograms " + score + " cleared", 0, "", submitScoreCallback);	
	}
	void submitScoreCallback(bool success)
	{
		Debug.Log("Submit Score Callback worked? " + success);
	}
}
