using UnityEngine;
using System.Collections;
using GameJolt.API.Objects;

public class Menu : MonoBehaviour 
{
	bool isSignedIn = false;
	bool leader = false;

	public GameObject buttonSignIn;
	public GameObject buttonGuest;

	public GameObject buttonStartGame;
	public GameObject buttonLeaderboards;

	void Start () 
	{
		isSignedIn = (GameJolt.API.Manager.Instance.CurrentUser != null);
		Debug.Log("Signed in? " + isSignedIn);

		//GameJolt.API.Objects.User user = new GameJolt.API.Objects.User("DeeCeptor", "Quadruple4");
		//user.SignIn(signInCallback);
	}

	public void SignIn()
	{
		GameJolt.UI.Manager.Instance.ShowSignIn(signInCallback); 
	}
	public void ShowLeaderboards()
	{
		leader = true;
		GameJolt.UI.Manager.Instance.ShowLeaderboards(leaderboardCallback);
	}
	public void StartGame()
	{
		Application.LoadLevel ("KevinLevel");
	}
	public void HowToPlay()
	{
		Application.LoadLevel ("OtherLevel");
	}

	public void leaderboardCallback(bool success)
	{
		Debug.Log("Leaderboard " + success);
	}
	public void signInCallback(bool success)
	{
		if (success) 
		{
			Debug.Log("The user signed in!");
			isSignedIn = true;
		}
		else
		{
			Debug.Log("Closed window or failed to sign in");
		}
	}
	public void changeSignIn(bool value)
	{
		isSignedIn = value;
	}

	
	void Update () 
	{
	
	}


	void OnGUI()
	{
		if(!leader)
		{
			if (!isSignedIn)
			{
				buttonSignIn.SetActive(true);
				buttonGuest.SetActive(true);
				//buttonSignIn.SetActive = true;
					/*
				if (GUI.Button(new Rect(300,100,200,50), "Sign into Gamejolt"))
					SignIn();
				if (GUI.Button(new Rect(300,200,200,50), "Play as Guest"))
					isSignedIn = true;
					*/
			}
			else
			{
				//create other buttons
				buttonStartGame.SetActive(true);
				buttonLeaderboards.SetActive(true);

					/*
				if (GUI.Button(new Rect(300,100,200,50), "Start Game"))
					StartGame();			
				if (GUI.Button(new Rect(300,200,200,50), "How to Play"))
					HowToPlay();
				if (GUI.Button(new Rect(300,50,200,50), "Show Leaderboard"))
					ShowLeaderboards();
					*/
			}
		}
	}
}
