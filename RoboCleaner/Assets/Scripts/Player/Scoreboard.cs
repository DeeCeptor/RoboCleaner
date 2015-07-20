using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour 
{
	public enum ScoreType { DEBRIS, TICKET, OTHER };

	public static Scoreboard board;

	public int lives = 2;	// How many lives we got. Can't revive if we're out of lives.
	[HideInInspector]
	public float time;

	public int debrisGotten = 0;
	private int shipsTicketed = 0;

	public List<AudioClip> multiplierSounds;
	public List<string> multiplierAnnouncements;

	public GameObject livesText;
	public GameObject scoreText;
	public GameObject PauseMenu;
	public GameObject timeText;
	public Slider cleaniplier;
	public Text multiplierText;

	public bool gameOver = false;

	public float multiplier = 1;
	private float nextMultiplierSound = 2.0f;
	private int score;	// Value >= 0

	// TROPHIES
	bool helper, janitor, cleaner, MrClean = false;	// Debris trophies
	bool greenShirt, insurable, survivor = false;	// Time survival trophies
	public bool died, lazored = false;	// Die once, Die from lazor beam
	bool ticketMaster = false; // Issue 30 tickets

	public 

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

			timeText.GetComponent<Text>().text = getFormattedTime(time);
			int minutes = (int) ((time) / 60.0f);

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


	public string getFormattedTime(float in_time)
	{
		int minutes = (int) ((in_time) / 60.0f);
		int seconds = (int) (in_time % 60);
		int milliseconds = (int) ((in_time - (minutes * 60) - seconds) * 100);
		return ("" + minutes).PadLeft(2, '0') + ":" + ("" + seconds).PadLeft(2, '0') + "." + ("" + milliseconds).PadLeft(2, '0');
	}


	public int getModifiedScore(int initial_score)
	{
		return (int) (initial_score * multiplier);
	}


	public void ReturnToMenu()
	{
		Time.timeScale = 1;
		StartCoroutine(loadMenu());
	}
	public IEnumerator loadMenu()
	{
		GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
		Debug.Log(objs.Length);
		for (int x = 0; x < objs.Length; x++) {
			GameObject.DestroyImmediate(objs[x]);
		}
		Application.LoadLevel ("Menu");
		yield return new WaitForSeconds(0f);
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
		Vector3 pos = new Vector3(
			Camera.main.ScreenToWorldPoint(multiplierText.transform.position).x,
			Camera.main.ScreenToWorldPoint(multiplierText.transform.position).y,
			0);
		spawnMovingText(pos, "+0.2!", Vector3.up * 2, 40).transform.SetParent(Camera.main.transform);

		if (multiplier >= nextMultiplierSound && multiplierSounds.Count > 0)
		{
			// Play sound, increase amount we need for next multiplier sound
			nextMultiplierSound++;
			AudioSource.PlayClipAtPoint(multiplierSounds[0], Camera.main.transform.position);
			multiplierSounds.RemoveAt(0);	// Remove the sound so we don't hear it again

			// Show announcement text on screen
			Vector3 posi = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, -1));
			posi.z = -1;
			string text = multiplierAnnouncements[0];
			multiplierAnnouncements.RemoveAt(0);
			spawnMovingText(posi, text, Vector3.down * 10, 80).transform.SetParent(Camera.main.transform);
		}
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
		multiplierText.text = "X" + multiplier.ToString("0.00");
	}


	public GameObject spawnMovingText(Vector3 location, string message, Vector3 velocity)
	{
		GameObject score = Instantiate(Resources.Load("FloatingScore", typeof(GameObject))) as GameObject;
		score.GetComponent<Rigidbody2D>().velocity = velocity;
		score.GetComponent<TextMesh>().text = message;
		score.transform.position = location;
		return score;
	}
	public GameObject spawnMovingText(Vector3 location, string message, Vector3 velocity, int size)
	{
		GameObject score = Instantiate(Resources.Load("FloatingScore", typeof(GameObject))) as GameObject;
		score.GetComponent<Rigidbody2D>().velocity = velocity;
		score.GetComponent<TextMesh>().text = message;
		score.GetComponent<TextMesh>().fontSize = size;
		score.transform.position = location;
		return score;
	}
	
	
	public IEnumerator modifyScore(int amount, ScoreType type)
	{
		score += amount;
		score = Mathf.Max(0, score);	// Score can't go below 0
		scoreText.GetComponent<Text>().text = "" + score;

		cleaniplier.value += ((float) amount) / (multiplier * 400);
		if (cleaniplier.value >= 1)
			addMultiplierLevel();

		if (amount > 0 && type == ScoreType.DEBRIS)
			debrisGotten++;
		else if (type == ScoreType.TICKET)
			shipsTicketed++;

		if (shipsTicketed > 29 && !ticketMaster)
		{
			unlockTrophy(35684);
			ticketMaster = true;
		}

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
		submitDebris();
	}
	void submitDebris()
	{
		GameJolt.API.Scores.Add(debrisGotten, debrisGotten + " gotten", 83163, "", submitDebrisCallback);	
	}
	void submitDebrisCallback(bool success)
	{
		Debug.Log("Submit Debris Callback worked? " + success);
		submitTime();
	}
	void submitTime()
	{
		int seconds = (int) time;
		GameJolt.API.Scores.Add(seconds, getFormattedTime(time), 83936, "", submitTimeCallback);	
	}
	void submitTimeCallback(bool success)
	{
		Debug.Log("Submit Time Callback worked? " + success);
		submitTickets();
	}
	void submitTickets()
	{
		GameJolt.API.Scores.Add(shipsTicketed, shipsTicketed + " angry captains", 83937, "", submitTicketsCallback);	
	}
	void submitTicketsCallback(bool success)
	{
		Debug.Log("Submit Tickets Callback worked? " + success);
	}
}
