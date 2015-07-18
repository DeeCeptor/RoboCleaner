using UnityEngine;
using System.Collections;
using GameJolt.API.Objects;

public class Menu : MonoBehaviour 
{
	bool isSignedIn = false;
	bool leader = false;

	void Start () 
	{
		isSignedIn = (GameJolt.API.Manager.Instance.CurrentUser != null);
		Debug.Log("Signed in? " + isSignedIn);

		//GameJolt.API.Objects.User user = new GameJolt.API.Objects.User("DeeCeptor", "Quadruple4");
		//user.SignIn(signInCallback);
	}

	void SignIn()
	{
		GameJolt.UI.Manager.Instance.ShowSignIn(signInCallback); 
	}
	void ShowLeaderboards()
	{
		leader = true;
		GameJolt.UI.Manager.Instance.ShowLeaderboards(leaderboardCallback);
	}
	void StartGame()
	{
		Application.LoadLevel ("KevinLevel");
	}
	void HowToPlay()
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

	
	void Update () 
	{
	
	}


	void OnGUI()
	{
		if(!leader)
		{
		if (!isSignedIn)
		{
			if (GUI.Button(new Rect(300,100,200,50), "Sign into Gamejolt"))
				SignIn();
			if (GUI.Button(new Rect(300,200,200,50), "Play as Guest"))
				isSignedIn = true;
		}
		else
		{
			if (GUI.Button(new Rect(300,100,200,50), "Start Game"))
				StartGame();			
			if (GUI.Button(new Rect(300,200,200,50), "How to Play"))
				HowToPlay();
			if (GUI.Button(new Rect(300,50,200,50), "Show Leaderboard"))
				ShowLeaderboards();
		}
		}
	}
}
