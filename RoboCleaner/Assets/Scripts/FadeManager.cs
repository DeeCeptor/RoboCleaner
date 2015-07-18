using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour {
	public static FadeManager fader;
	public Texture blackTexture;

	private float alphaFadeValue = 0;
	private bool fadingIn = false;
	private bool fadingOut = false;
	private float fadingOverTime;


	void Start () 
	{
		fader = this;
		fadeIn(5);
	}


	/**
	 * Starts black, goes to not black
	 */ 
	public void fadeIn(float overTime)
	{
		alphaFadeValue = 1;
		fadingOverTime = overTime;
		fadingIn = true;
		fadingOut = false;
	}
	/**
	 * Starts transparent, fading to black
	 * Permanent means it remains black after fading out
	 */ 
	public void fadeOut(float overTime, bool permanent)
	{
		alphaFadeValue = 0;
		fadingOverTime = overTime;
		fadingOut = true;
		fadingIn = false;
	}

	void Update () 
	{

	}

	void OnGUI(){
		if (fadingIn)
		{
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / fadingOverTime);
			GUI.color = new Color(0, 0, 0, alphaFadeValue); 
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture );

			if (alphaFadeValue <= 0)
			{
				fadingIn = false;
			}
		}
		else if (fadingOut)
		{
			alphaFadeValue += Mathf.Clamp01(Time.deltaTime / fadingOverTime);
			GUI.color = new Color(0, 0, 0, alphaFadeValue); 
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture );
			
			if (alphaFadeValue >= 1)
			{
				fadingOut = false;
			}
		}
	}
}
