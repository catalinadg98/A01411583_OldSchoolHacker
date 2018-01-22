using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
	//Variables
	const string menuHint = "You can type menu at any time.";

	//Class attributes
	//These arrays will hold the passwords of the diffrent game's levels
	string[] level1Passwords = { "book", "class", "teacher", "room", "hour" };
	string[] level2Passwords = { "cashier", "department", "payment", "electronics" };
	string[] level3Passwords = { "dossier", "international", "security" };


	//Game state variables
	int count = 0;
	int level;
	enum gameState { MainMenu, Password, Win };
	gameState currentScreen = gameState.MainMenu;
	string password;

	bool changePassword;

	// Use this for initialization
	void Start () {
		ShowMainMenu ();
	}

	// Update is called once per frame
	void Update () {

	}

	void ShowMainMenu()
	{
		//We clear the screen
		currentScreen = gameState.MainMenu;
		Terminal.ClearScreen ();

		//We show the menu
		Terminal.WriteLine ("What do you want to hack today?");
		Terminal.WriteLine ("1. Town's college");
		Terminal.WriteLine ("2. City's Super Center");
		Terminal.WriteLine ("3. NSA server");
		Terminal.WriteLine ("Option?");

		//We set our current screen state as the main menu
		currentScreen = gameState.MainMenu;

		changePassword = true;
	}

	//The OnUserInput maethod is special. It is called everytime the user
	//hits enter on their keyboard. This method will let us evaluate the
	//inout data and act accordingly.
	void OnUserInput(string input)
	{
		//If user inputs the "menu" keyword, then we call the
		//ShowMainMenu() methos once more
		if (input == "menu") {
			ShowMainMenu ();
		}
		//However, ig the user types quit, close, exit, then we try to close
		//out game. If the game is played on a Web browser, then we ask the
		//user to close the browser's tab
		else if (input == "quit" || input == "close" || input == "exit") {

			Terminal.WriteLine ("Please, close the browser's tab");
			Application.Quit ();
		}
		//If the user inputs anything that is not menu, quit, close or exit,
		//then we are going to handle that input depending on the game state.
		//If the game state is still MainMenu, then we call the RunMainMenu()
		//method.
		else if (currentScreen == gameState.MainMenu) {
			RunMainMenu (input);
		}
		//But if the current game state is Password, then we call the
		//CheckPassword() method
		else if (currentScreen == gameState.Password) {
			checkPassword (input);
		}
	}

	private void checkPassword(string input)
	{
		if (input == password) {
			count = 0;
			displayWinScreen ();
		} else {
			if (count > 0) {
				displayLoScreen ();
			} else {
				AskForPassword ();
			}
		}

	}

	private void displayLoScreen()
	{
		Terminal.WriteLine ("You failed, loser!");
	}

	private void displayWinScreen()
	{
		currentScreen = gameState.Win;
		Terminal.ClearScreen ();
		ShowLevelReward ();
		Terminal.WriteLine (menuHint);
	}



	private void ShowLevelReward()
	{
		switch (level) {
		case 1:
			Terminal.WriteLine ("Have a book...");
			Terminal.WriteLine (@"
    _______
   /      //
  /      //
 /______//
(______(/
            ");
			break;
		case 2:
			Terminal.WriteLine ("Grab an apple!");
			Terminal.WriteLine (@"
      /
     |~
  ___|___
 /       \
|         )
|         )
 \_______/
            ");
			break;
		case 3:
			Terminal.WriteLine ("Greetings...");
			Terminal.WriteLine (@"
 _ __   ___  __ _
| '_ \ / __|/ _` |
| | | |\__ \ (_| |
|_| |_||___)\__,_|
            ");
			Terminal.WriteLine ("Welcome to the NSA server");
			break;
		default:
			Debug.LogError ("Invalid level reached.");
			break;
		}
	}

	void RunMainMenu(string input)
	{
		//Validate that input is valid
		bool isValidInput = (input == "1") || (input == "2") || (input == "3");
		//If it is then we convert the input into an int and assign it to level
		//and call askForPassword();
		if (isValidInput) {
			level = int.Parse (input);
			AskForPassword ();
		} else if (input == "007") {
			Terminal.WriteLine ("Please enter a valid level, Mr. Bond");
		} else if (input == "707") {
			Terminal.WriteLine ("I know it's you Seven, please enter a valid level");
		} else if (input == "24601") {
			Terminal.WriteLine ("Now prisoner 24601, your time is up. \nEnter a valid level ");
		}	
		else
		{
			Terminal.WriteLine("Enter a valid level");
		}
	}


	private void AskForPassword()
	{
		//We set our current game state as Password
		currentScreen = gameState.Password;
		count++;
		//We set our current terminal screen
		Terminal.ClearScreen();

		//We call the SetRandomPassword() method
		if (changePassword) {

			SetRandomPassword ();
		}

		Terminal.WriteLine ("Enter your password. Hint: " + password.Anagram ());
		Terminal.WriteLine (menuHint);
	}

	private void SetRandomPassword()
	{
		changePassword = false;

		switch (level) 
		{
		case 1:
			password = level1Passwords [UnityEngine.Random.Range (0, level1Passwords.Length)];
			break;
		case 2:
			password = level2Passwords [UnityEngine.Random.Range (0, level2Passwords.Length)];
			break;
		case 3:
			password = level3Passwords [UnityEngine.Random.Range (0, level3Passwords.Length)];
			break;
		default:
			Debug.LogError ("Invalid level. How did you manage that?");
			break;
		}
	}
}