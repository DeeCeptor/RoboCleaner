using UnityEngine;
using System.Collections;

public class AIZoomSpawn : MonoBehaviour {
	public Transform toSpawn;
	public GameObject[] ZoombaList;
	public int ZoombaNum = 2;
	public bool menu = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		ZoombaList = GameObject.FindGameObjectsWithTag("AIZoomba");
		if (ZoombaList.Length<ZoombaNum)
		{
		if(menu == false)
		{
			Instantiate(toSpawn, new Vector3(transform.position.x + Random.Range(-2,2),transform.position.y + Random.Range(-2,2),transform.position.z), transform.rotation);
		}
			if(menu == true)
			{
				GameObject newZoomba = (GameObject) Instantiate((GameObject) Resources.Load("AIZoombaSpawner", typeof(GameObject)), this.transform.position, Quaternion.identity);
				Camera.main.GetComponent<CameraFollow>().target = newZoomba.transform;
			}
		}
	}
}
