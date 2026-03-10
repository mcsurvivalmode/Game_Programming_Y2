using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


//game status data structure
[Serializable]
public struct GameStatus
{
	public string playerName;
	public int currentLevel;
	public string spawnPoint;
	public int health;
	public int coinsCollected;
	public List<Vector3> NPCs;
	public Vector3 playerPosition;
}

// Create Game Class by extending MonoBehaviour
public class MB_GameManager : MonoBehaviour
{
	// Declare Struct for GameStatus (HUD Data)
	public GameStatus gameStatus;
	// Variable for file path
	string filePath;
	// Variable for filename
	const string FILE_NAME = "SaveStatus.json";

	// Use this for initialization
	public void Start()
	{
		//retrieving saving location
		filePath = Application.persistentDataPath;
		gameStatus = new GameStatus();
		Debug.Log(filePath);
		//startup initialisation
		LoadGameStatus();
	}

	//this function loads a saving file if found
	public void LoadGameStatus()
	{
		//always check the file exists
		if (File.Exists(filePath + "/" + FILE_NAME))
		{
			//load the file content as string
			string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
			//deserialise the loaded string into a GameStatus struct
			gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
			Debug.Log("File loaded successfully");
		}
		else
		{
			//initilise a new game status
			resetGame();
			Debug.Log("File not found");
		}
	}

	public void resetGame()
	{
		//initilise a new game status
		gameStatus.playerName = "Keith";
		gameStatus.currentLevel = 1;
		gameStatus.spawnPoint = "Beginning";//reference to a game object
		gameStatus.health = 100;
		gameStatus.coinsCollected = 0;
		gameStatus.playerPosition = new Vector3(0, 0, 0);
		gameStatus.NPCs = new List<Vector3>(){  new Vector3(22.0f,0.5f,-22.0f),
											new Vector3(0.0f,0.5f,-22.0f),
											new Vector3(-22.0f,0.5f,-22.0f),
											new Vector3(22.0f,0.5f,0.0f),
											new Vector3(-22.0f,0.5f,0.0f),
											new Vector3(-22.0f,0.5f,22.0f),
											new Vector3(0.0f,0.5f,22.0f),
											new Vector3(22.0f,0.5f,22.0f)};
		// Save initalisation scores
		SaveGameStatus();
	}

	//this function overrides the saving file
	public void SaveGameStatus()
	{
		//serialise the GameStatus struct into a Json string
		string gameStatusJson = JsonUtility.ToJson(gameStatus);
		//write a text file containing the string value as simple text
		File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
		Debug.Log("File created and saved");
	}

	//build our UI controls- a simple label
	public string UpdateStatus()
	{
		//building the formatted string to be shown to the user
		string message = "";
		message += "Player Name: " + gameStatus.playerName + "\n";
		message += "Current Level: " + gameStatus.currentLevel + "\n";
		message += "Spawn Point: " + gameStatus.spawnPoint + "\n";
		message += "Health: " + gameStatus.health + "\n";
		message += "Coins: " + gameStatus.coinsCollected + "\n";
		//Debug.Log(message);
		return message;

		
	}
	
}