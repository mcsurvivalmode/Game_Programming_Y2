using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


//game status data structure
[Serializable]
public struct JSON_GameStatus
{
    public string spawnPoint;

    public int Deaths;
    public int health;
    public int coinsCollected;
    public Vector3 playerPosition;
    public Vector3 coinPosition;
}

// Create Game Class by extending MonoBehaviour
public class JSON_GameManager : MonoBehaviour
{
    
    // Declare Struct for GameStatus (HUD Data)
    public JSON_GameStatus JgameStatus;
    // Variable for file path
    string filePath;
    // Variable for filename
    const string FILE_NAME = "DataStatus.json";

    // Use this for initialization
    public void Start()
    {
        //retrieving saving location
        filePath = Application.persistentDataPath;
        JgameStatus = new JSON_GameStatus();
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
            JgameStatus = JsonUtility.FromJson<JSON_GameStatus>(loadedJson);
            GameObject.Find("Player").transform.position = JgameStatus.playerPosition;

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

        JgameStatus.spawnPoint = "Beginning";//reference to a game object
        JgameStatus.health = 10;
        JgameStatus.Deaths = 0;
        JgameStatus.coinsCollected = 0;
        JgameStatus.playerPosition = new Vector3(0, 0, 0);
        JgameStatus.coinPosition = new Vector3(1, 1, 1);
        GameObject.Find("Player").transform.position = JgameStatus.playerPosition;
        
        Debug.Log("File reset");
        // Save initalisation scores
        SaveGameStatus();
    }

    //this function overrides the saving file
    public void SaveGameStatus()
    {
        //serialise the GameStatus struct into a Json string
        string gameStatusJson = JsonUtility.ToJson(JgameStatus);
        //write a text file containing the string value as simple text
        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
        Debug.Log("File created and saved");
        JgameStatus.playerPosition = GameObject.Find("Player").transform.position;
    }

    //build our UI controls- a simple label
    public string UpdateStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";
        message += "Spawn Point: " + JgameStatus.spawnPoint + "\n";
        message += "Health: " + JgameStatus.health + "\n";
        message += "Deaths: " + JgameStatus.Deaths + "\n";
        message += "Your HighScore: " + JgameStatus.coinsCollected + "\n";
        //Debug.Log(message);
        return message;


    }

}