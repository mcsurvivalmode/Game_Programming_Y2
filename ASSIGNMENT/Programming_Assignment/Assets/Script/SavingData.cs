using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

//game status data structure
[Serializable]
public struct GameStatus_EX
{
	public int currentLevel;
	public int health;
	public int coins;
}

public class SavingData : MonoBehaviour
{

	GameStatus_EX gameStatus;
	string filePath;
	const string FILE_NAME = "SaveStatus.json";

	//build our UI controls- a simple label
	void ShowStatus ()
	{
		//building the formatted string to be shown to the user
		string message = "";
		message += "Current Level: " + gameStatus.currentLevel + "\n";
		message += "Health: " + gameStatus.health + "\n";
		message += "Coins: " + gameStatus.coins + "\n";

		GetComponent<Text> ().text = message;
	}


	//this function overrides the saving file
	public void SaveGameStatus ()
	{
		//serialise the GameStatus struct into a Json string
		string gameStatusJson = JsonUtility.ToJson (gameStatus);
		//write a text file containing the string value as simple text
		File.WriteAllText (filePath + "/" + FILE_NAME, gameStatusJson);
		Debug.Log ("File created and saved");
	}

	//this function loads a saving file if found
	public void LoadGameStatus ()
	{
		//always check the file exists
		if (File.Exists (filePath + "/" + FILE_NAME)) {
			//load the file content as string
			string loadedJson = File.ReadAllText (filePath + "/" + FILE_NAME);
			//deserialise the loaded string into a GameStatus struct
			gameStatus = JsonUtility.FromJson<GameStatus_EX> (loadedJson);
			Debug.Log ("File loaded successfully");
		} else {
			//initilise a new game status
			gameStatus.currentLevel = 1;
			gameStatus.health = 100;
			gameStatus.coins = 0;
			Debug.Log ("File not found");
		}
	}

	// Use this for initialization
	void Start ()
	{
		//retrieving saving location
		filePath = Application.persistentDataPath;
		gameStatus = new GameStatus_EX ();
		Debug.Log (filePath);
		//startup initialisation
		LoadGameStatus ();
	}

	// Update is called once per frame
	void Update ()
	{
		ShowStatus ();
	}

}