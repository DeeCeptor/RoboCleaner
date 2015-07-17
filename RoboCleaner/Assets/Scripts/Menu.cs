using UnityEngine;
using System.Collections;
using GameJolt.API.Objects;

public class Menu : MonoBehaviour {

	void Start () 
	{
		bool isSignedIn = (GameJolt.API.Manager.Instance.CurrentUser != null);
		Debug.Log("Signed in? " + isSignedIn);

		GameJolt.API.Objects.User user = new GameJolt.API.Objects.User("DeeCeptor", "Quadruple4");
		user.SignIn(signInCallback);
		//GameJolt.UI.Manager.Instance.ShowSignIn(signInCallback); 
	}

	public void signInCallback(bool success)
	{
		if (success) 
		{
			Debug.Log("The user signed in!");
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
		if (GUI.Button(new Rect(10,10,50,50), "Show Leaderboard"))
			GameJolt.UI.Manager.Instance.ShowLeaderboards();

	}
}
