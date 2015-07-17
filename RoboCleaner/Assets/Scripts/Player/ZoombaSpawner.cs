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

		// Wait a few seconds before reviving player
		yield return new WaitForSeconds(3f);

		Debug.Log("Spawning new zoomba");

		Scoreboard.board.lives--;	// Remove a life
		
		// Create a new Zoomba at this position
		GameObject newZoomba = (GameObject) Instantiate(ZoombaToSpawn, this.transform.position, Quaternion.identity);
		Camera.main.GetComponent<CameraFollow>().target = newZoomba.transform;

		// Give invulnerability
		newZoomba.GetComponent<PlayerController>().makeInvulnerable(5);
	}
	IEnumerator GameOver()
	{
		Debug.Log("Starting game over sequence");

		// Wait a bit before kicking the player out
		yield return new WaitForSeconds(3f);

		Debug.Log("GAME OVER!");
	}
}
