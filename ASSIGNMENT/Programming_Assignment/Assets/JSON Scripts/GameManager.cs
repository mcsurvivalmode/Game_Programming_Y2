using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using System.Collections;
using System.Collections.Generic;

//game status data structure
[Serializable]
public struct GameStatus
{
    public string playerName;
    public string location;
    public string status;
    public int coins;
}

[Serializable]
public class GameManager : MonoBehaviour
{
    GameStatus gameStatus;
    string filePath;
    const string FILE_NAME = "SaveData.json";
    //build our UI controls- a simple label

    CharacterMovement coinsCollected;


    void ShowStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";
        message += "Player Name: " + gameStatus.playerName + "\n";
        message += "Location: " + gameStatus.location + "\n";
        message += "Health: " + gameStatus.status + "\n";
        message += "Coins: " + gameStatus.coins + "\n";
        GetComponent<TMP_Text>().text = message;
    }
    //this function emulates a random game event that changes the player's statistics
    public void NewGameStatus()
    {
        //this will create a new game
        gameStatus.playerName = "Walter";
        gameStatus.location = "Tutorial";//reference to a game object
        gameStatus.status = "Safe";
        gameStatus.coins = 0;
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
            gameStatus.playerName = "Player";
            gameStatus.location = "Tutorial";//reference to a game object
            gameStatus.status = "Safe";
            gameStatus.coins = 0;
            Debug.Log("File not found");
        }
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
    // Use this for initialization
    void Start()
    {
        //retrieving saving location
        filePath = Application.persistentDataPath;
        gameStatus = new GameStatus();
        Debug.Log(filePath);
        //startup initialisation
        LoadGameStatus();
    }
    // Update is called once per frame
    void Update()
    {
        ShowStatus();
        
        
    }
}