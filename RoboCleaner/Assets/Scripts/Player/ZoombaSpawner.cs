using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Spawns new zoombas when the player dies at this object's position.
 */ 
public class ZoombaSpawner : MonoBehaviour 
{
	public static ZoombaSpawner spawner;

	void Start () 
	{
		spawner = this;
	}
	
	void Update () 
	{
	
	}


	public void PlayerDied()
	{
		if (Scoreboard.board.lives > 0)
			StartCoroutine(Revive());
		else
			StartCoroutine(GameOver());
	}
	IEnumerator Revive()
	{
		Debug.Log("Died! Beginning spawning process");

		FadeManager.fader.fadeOut(6, true);
		// Wait a few seconds before reviving player
		yield return new WaitForSeconds(3f);
		FadeManager.fader.fadeIn(5);

		Debug.Log("Spawning new zoomba");

		Scoreboard.board.modifyLivesBy(-1);	// Remove a life
		
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
		yield return new WaitForSeconds(3f);

		// Only happens if we had a game over, so show the game over text
		GameObject.Find("UICanvas/GameOver").GetComponent<Image>().enabled = true;
		GameObject.Find("UICanvas/GameOver/QuitToMenuButton").SetActive(true);

		ShowLeaderboards();

		Debug.Log("GAME OVER!");
		//Application.LoadLevel("Menu");
	}


	
	public void ShowLeaderboards()
	{
		if (GameJolt.API.Manager.Instance != null && GameJolt.API.Manager.Instance.CurrentUser != null)	// Only submit score if we're logged in
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
