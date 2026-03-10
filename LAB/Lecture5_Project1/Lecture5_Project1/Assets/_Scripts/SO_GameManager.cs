using UnityEngine;
using System.Collections.Generic;

// Game Status Struct
public struct GameStatus_SO
{
	public string playerName;
	public int currentLevel;
	public string spawnPoint;
	public int health;
	public int coinsCollected;
	public List<Vector3> NPCs;
	public Vector3 playerPosition;
}

// Create asset menu item called GameManager
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManager_SO", order = 1)]

// Create Game Manager class that extends ScriptableObject
public class SO_GameManager : ScriptableObject
{
	// Declare Struct for GameStatus (HUD Data)
	public GameStatus gameStatus;

	// Use this for initialization
	public void Start()
	{
		LoadGameStatus();
	}

	//this function loads a saving file if found
	public void LoadGameStatus()
	{
		// Check for previous play or death!
		if (gameStatus.playerName == null || gameStatus.health <= 0)
		{
			// If new game, create new struct
			gameStatus = new GameStatus();
			//initilise a new game status
			resetGame();
			Debug.Log("File not found");
		}
		else
		{
			// Do nothing
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
		return message;
	}

}

