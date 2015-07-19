using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Spawns new zoombas when the player dies at this object's position.
 */ 
public class ZoombaSpawner : MonoBehaviour 
{
	public static ZoombaSpawner spawner;
	public Transform deathmessage;

	void Start () 
	{
		spawner = this;

		StartCoroutine(timeToClean());
	}
	
	void Update () 
	{
	
	}

	IEnumerator timeToClean()
	{
		yield return new WaitForSeconds(1.0f);

		GameObject timeToClean = GameObject.Find("UICanvas").transform.FindChild("TimeToClean").gameObject;
		timeToClean.GetComponent<Image>().enabled = true;
		timeToClean.GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(1.8f);

		timeToClean.GetComponent<Image>().enabled = false;
	}


	public void PlayerDied()
	{
		if (Scoreboard.board.lives > 0)
		{
			
			StartCoroutine(Revive());
		}
		else
			StartCoroutine(GameOver());
	}
	IEnumerator Revive()
	{
		Debug.Log("Died! Beginning spawning process");

		SceneFadeInOut.fader.EndScene();

		// Wait a few seconds before reviving player
		yield return new WaitForSeconds(1.5f);

		//FadeManager.fader.fadeIn(5);
		SceneFadeInOut.fader.StartScene();

		Debug.Log("Spawning new zoomba");

		Scoreboard.board.modifyLivesBy(-1);	// Remove a life

		StartCoroutine(timeToClean());

		// Create a new Zoomba at this position
		GameObject newZoomba = (GameObject) Instantiate((GameObject) Resources.Load("Zoomba3000", typeof(GameObject)), this.transform.position, Quaternion.identity);
		Camera.main.GetComponent<CameraFollow>().target = newZoomba.transform;

		// Give invulnerability
		newZoomba.GetComponent<PlayerController>().makeInvulnerable(5);
	}
	IEnumerator GameOver()
	{
		Debug.Log("Starting game over sequence");
		//FadeManager.fader.fadeOut(6, true);
		GameObject.Find("Main Camera").transform.FindChild("Very Far Stars").gameObject.SetActive(true);

		Scoreboard.board.gameOver = true;
		Scoreboard.board.submitScore();
		// Wait a bit before kicking the player out
		yield return new WaitForSeconds(5f);

		// Only happens if we had a game over, so show the game over text
		GameObject.Find("UICanvas/GameOver").GetComponent<Image>().enabled = true;
		GameObject.Find("UICanvas/GameOver/QuitToMenuButton").SetActive(true);

		ShowLeaderboards();

		Debug.Log("GAME OVER!");
	}


	
	public void ShowLeaderboards()
	{
		if (GameJolt.API.Manager.Instance != null)	// Only submit score if we're logged in
			GameJolt.UI.Manager.Instance.ShowLeaderboards(leaderBoardCallback);
	}
	public void leaderBoardCallback(bool success)
	{
		if (!success)
			ReturnToMenu();
	}
	public void ReturnToMenu()
	{
		Application.LoadLevel("Menu");
	}
}
