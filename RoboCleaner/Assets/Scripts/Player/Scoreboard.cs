using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour 
{
	public enum ScoreType { DEBRIS, TICKET, OTHER };

	public static Scoreboard board;

	public int lives = 1;	// How many lives we got. Can't revive if we're out of lives.
	public float time;

	public int debrisGotten = 0;
	private int shipsTicketed = 0;

	public GameObject livesText;
	public GameObject scoreText;
	public GameObject PauseMenu;
	public GameObject timeText;
	public Slider cleaniplier;
	public Text multiplierText;

	public bool gameOver = false;

	public float multiplier = 1;
	public float multiplierLevel = 0;
	private int score;	// Value >= 0

	// TROPHIES
	bool helper, janitor, cleaner, MrClean = false;	// Debris trophies
	bool greenShirt, insurable, survivor = false;	// Time survival trophies
	public bool died, lazored = false;	// Die once, Die from lazor beam


	void Start () 
	{
		board = this;

		modifyLivesBy(0);
		modifyScore(0, Scoreboard.ScoreType.OTHER);
	}


	void Update () 
	{
		if (Input.GetButtonDown("Pause") && !gameOver)
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

		// Not paused and not game over
		if (Time.timeScale != 0 && !gameOver)
		{
			// Timer is running if we're not paused
			time += Time.deltaTime;

			int minutes = (int) ((time) / 60.0f);
			int seconds = (int) (time % 60);
			int milliseconds = (int) ((time - (minutes * 60) - seconds) * 100);
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


			// Update cleaniplier
			cleaniplier.value -= Time.deltaTime / 10;
			if (cleaniplier.value <= 0)
				lowerMultiplierLevel();
		}
	}


	public int getModifiedScore(int initial_score)
	{
		return (int) (initial_score * multiplier);
	}


	public void ReturnToMenu()
	{
		Application.LoadLevel("Menu");
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


	public void addMultiplierLevel()
	{
		cleaniplier.value = 0.4f;
		multiplier += 0.2f;
		setMultiplierText();
	}
	public void lowerMultiplierLevel()
	{
		if (multiplier > 1.0f)
		{
			// Lower level if we can
			cleaniplier.value = 0.9f;
			multiplier -= 0.2f;
			setMultiplierText();
		}
	}
	public void setMultiplierText()
	{
		multiplierText.text = multiplier + "X";
	}


	public IEnumerator modifyScore(int amount, ScoreType type)
	{
		score += amount;
		score = Mathf.Max(0, score);	// Score can't go below 0
		scoreText.GetComponent<Text>().text = "" + score;

		cleaniplier.value += ((float) amount) / 100;
		if (cleaniplier.value >= 1)
			addMultiplierLevel();

		if (amount > 0 && type == ScoreType.DEBRIS)
			debrisGotten++;
		else if (type == ScoreType.TICKET)
			shipsTicketed++;

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

		yield return new WaitForSeconds(0f);
	}


	public void unlockTrophy(int trophyID)
	{
		// First check if we already have unlocked this trophy
		if (GameJolt.API.Manager.Instance != null && GameJolt.API.Manager.Instance.CurrentUser != null)
			GameJolt.API.Trophies.Get(trophyID, getTrophy);
	}
	public void getTrophy(GameJolt.API.Objects.Trophy trophy)
	{
		Debug.Log ("Got trophy: " + trophy.ID + " unlocked: " + trophy.Unlocked);
		if (!trophy.Unlocked)	// Unlock trophy if we don't already have it
			actuallyUnlockTrophy(trophy.ID);
	}
	public void actuallyUnlockTrophy(int trophyID)
	{
		if (GameJolt.API.Manager.Instance != null && GameJolt.API.Manager.Instance.CurrentUser != null)	// Only submit score if we're logged in
		{
			GameJolt.API.Trophies.Unlock(trophyID, trophyCallback);
		}
	}
	public void trophyCallback(bool success)
	{
		Debug.Log("Got trophy: " + success);
	}


	public void submitScore()
	{
		if (GameJolt.API.Manager.Instance != null && GameJolt.API.Manager.Instance.CurrentUser != null)	// Only submit score if we're logged in
			GameJolt.API.Scores.Add(score, score + "", 0, "", submitScoreCallback);	
	}
	void submitScoreCallback(bool success)
	{
		Debug.Log("Submit Score Callback worked? " + success);
	}
}
