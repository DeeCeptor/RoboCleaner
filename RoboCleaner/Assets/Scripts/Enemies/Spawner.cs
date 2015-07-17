using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] redShips;
	public GameObject[] blueShips;
	public Transform[] blueSpawnable;
	public Transform[] redSpawnable;
	
	public Transform TopWall;
	public Transform BottomWall;
	public Transform RightWall;
	public Transform LeftWall;
	private float spawnDelay = 2.5f;
	public float spawnTimer = 0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(spawnTimer <= Time.time)
		{
			spawnTimer = Time.time + spawnDelay;
			blueShips = GameObject.FindGameObjectsWithTag("blue");
			redShips = GameObject.FindGameObjectsWithTag("red");
			if(blueShips.Length > redShips.Length)
			{
			Instantiate (redShips[Random.Range(0,redShips.Length-1)],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
			}
			else if(redShips.Length > blueShips.Length)
			{
				Instantiate (blueShips[Random.Range(0,redShips.Length-1)],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
			}
			else if(redShips.Length == blueShips.Length)
			{
				int rando = Random.Range (0,1);
				if(rando == 0)
				{
					Instantiate (redShips[Random.Range(0,redShips.Length-1)],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
				}
				if(rando == 1)
				{
					Instantiate (blueShips[Random.Range(0,redShips.Length-1)],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
				}
			}
			if(spawnDelay>0.25f)
			{
			spawnDelay = spawnDelay - 0.1f;
			}
		}
	
	
	}
}
