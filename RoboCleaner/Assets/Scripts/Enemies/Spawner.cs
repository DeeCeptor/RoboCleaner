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
	private float spawnDelay = 5f;
	public float spawnTimer = 0f;
	public float minSpawnTimer = 0.2f;
	public float timerLowerBy = 0.1f;
	public float frigateChanceStart = 1f;
	public float frigateIncrement = 0.5f;
	public float corvetteIncrement = 1f;
	public float corvetteChanceStart = 11f;
	public int maxShips = 40;
	public float difficultyincrements =5f;
	private float difficultyTimer =0;
	private int spawnNumber;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(spawnTimer <= Time.time)
		{
			spawnTimer = Time.time + spawnDelay;
			spawnNumber = Random.Range(1,101);
			blueShips = GameObject.FindGameObjectsWithTag("blue");
			redShips = GameObject.FindGameObjectsWithTag("red");
			if((redShips.Length + blueShips.Length) <=maxShips){
			if(blueShips.Length > redShips.Length)
			{
				if(spawnNumber>corvetteChanceStart)
				{
				Instantiate (redSpawnable[0],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
				}
				else if(spawnNumber>frigateChanceStart && spawnNumber < corvetteChanceStart)
				{
				Instantiate (redSpawnable[1],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
				}
				else
				{
				Instantiate (redSpawnable[2],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
				}
			}
			else if(redShips.Length > blueShips.Length)
			{
					if(spawnNumber>corvetteChanceStart)
					{
						Instantiate (blueSpawnable[0],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
					}
					else if(spawnNumber>frigateChanceStart && spawnNumber < corvetteChanceStart)
					{
						Instantiate (blueSpawnable[1],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
					}
					else
					{
						Instantiate (blueSpawnable[2],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
					}
			}
			else if(redShips.Length == blueShips.Length)
			{
				int rando = Random.Range (0,1);
				if(rando == 0)
				{
						if(spawnNumber>corvetteChanceStart)
						{
							Instantiate (redSpawnable[0],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
						}
						else if(spawnNumber>frigateChanceStart && spawnNumber < corvetteChanceStart)
						{
							Instantiate (redSpawnable[1],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
						}
						else
						{
							Instantiate (redSpawnable[2],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
						}
				}
				if(rando == 1)
				{
						if(spawnNumber>corvetteChanceStart)
						{
							Instantiate (blueSpawnable[0],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
						}
						else if(spawnNumber>frigateChanceStart && spawnNumber < corvetteChanceStart)
						{
							Instantiate (blueSpawnable[1],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
						}
						else
						{
							Instantiate (blueSpawnable[2],new Vector3(Random.Range (LeftWall.position.x,RightWall.position.x), Random.Range (BottomWall.position.y,TopWall.position.y),transform.position.z),transform.rotation);
						}				}
			}
			
		}
			
		}
		if(difficultyTimer < Time.time)
		{
			difficultyTimer = difficultyTimer + difficultyincrements;
			if(spawnDelay>minSpawnTimer)
			{
			spawnDelay = spawnDelay - timerLowerBy;
			}
			frigateChanceStart = frigateChanceStart + frigateIncrement;
			corvetteChanceStart = corvetteChanceStart + corvetteIncrement;
		}
		
	}
}
