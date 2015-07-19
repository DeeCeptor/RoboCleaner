using UnityEngine;
using System.Collections;


public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f; // Speed that the screen fades to and from black.
	
	private bool sceneStarting = true; // Whether or not the scene is still fading in.
	private bool sceneEnding = false;

	public GUITexture guiTexture;

	public static SceneFadeInOut fader;

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		fader = this;
	}
	
	void Update()
	{
		// If the scene is starting…
		if(sceneStarting)
			StartScene(); 		// … call the StartScene function.
		else if (sceneEnding)
			EndScene();
	}
	
	public void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	public void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	public void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		guiTexture.enabled = true;
		sceneStarting = true;
		sceneEnding = false;

		// If the texture is almost clear…
		if(guiTexture.color.a <= 0.05f)
		{
			// … set the colour to clear and disable the GUITexture.
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;

			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		guiTexture.enabled = true;
		sceneEnding = true;
		sceneStarting = false;

		// Start fading towards black.
		FadeToBlack();

		// If the texture is almost clear…
		if(guiTexture.color.a >= 0.99f)
		{
			// … set the colour to clear and disable the GUITexture.
			guiTexture.color = Color.black;

			// The scene is no longer starting.
			sceneEnding = false;
		}
	}
	
}