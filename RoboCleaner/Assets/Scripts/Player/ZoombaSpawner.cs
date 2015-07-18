using UnityEngine;
using System.Collections;

/**
 * Spawns new zoombas when the player dies at this object's position.
 */ 
public class ZoombaSpawner : MonoBehaviour 
{
	public static ZoombaSpawner spawner;
	public GameObject ZoombaToSpawn;

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

		FadeManager.fader.fadeOut(6);
		// Wait a few seconds before reviving player
		yield return new WaitForSeconds(3f);
		FadeManager.fader.fadeIn(5);

		Debug.Log("Spawning new zoomba");

		Scoreboard.board.modifyLivesBy(-1);	// Remove a life
		
		// Create a new Zoomba at this position
		GameObject newZoomba = (GameObject) Instantiate(ZoombaToSpawn, this.transform.position, Quaternion.identity);
		Camera.main.GetComponent<CameraFollow>().target = newZoomba.transform;

		// Give invulnerability
		newZoomba.GetComponent<PlayerController>().makeInvulnerable(5);
	}
	IEnumerator GameOver()
	{
		Debug.Log("Starting game over sequence");
		FadeManager.fader.fadeOut(6);
		Scoreboard.board.submitScore();
		// Wait a bit before kicking the player out
		yield return new WaitForSeconds(3f);

		Debug.Log("GAME OVER!");
		//Application.LoadLevel("Menu");
	}
}
