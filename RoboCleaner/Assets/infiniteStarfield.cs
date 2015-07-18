using UnityEngine;
using System.Collections;

public class infiniteStarfield : MonoBehaviour {

	private Transform tx;
	//this is the new one we're creating
	private ParticleSystem.Particle[] points;

	//this is the component we're populating
	private ParticleSystem field;

	public int starsMax = 100;
	public float starSize = 1;
	public float starDistance = 10;
	public float starClipDistance = 2;

	private float starDistanceSqr;
	private float starClipDistanceSqr;

	// Use this for initialization
	void Start () {
		tx = transform;
		starDistanceSqr = starDistance * starDistance;
		starClipDistanceSqr = starClipDistance * starClipDistance;
		field = GetComponent<ParticleSystem>();

		//make render behind

	
	}

	private void CreateStars()
	{

		points = new ParticleSystem.Particle[starsMax];

		for(int i = 0; i < starsMax; i++)
		{
			points[i].position = Random.insideUnitSphere * starDistance + tx.position;
			points[i].color = new Color(1,1,1,1);
			points[i].size = starSize;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (points == null) CreateStars();

		for (int i = 0; i < starsMax; i++)
		{
			if((points[i].position - tx.position).sqrMagnitude > starDistanceSqr)
			{
				points[i].position = Random.insideUnitSphere * starDistance + tx.position;
			}

			if(points[i].position.z < 0)
			{
				points[i].color = new Color(1,1,1,0);
			}

			/*
			if((points[i].position - tx.position).sqrMagnitude <= starClipDistanceSqr)
			{
				float percent = (points[i].position - tx.position).sqrMagnitude / starClipDistance;
				points[i].color = new Color(1,1,1,percent);
				points[i].size = percent * starSize;
			}*/
		}
		field.SetParticles (points, points.Length);
	}
}
