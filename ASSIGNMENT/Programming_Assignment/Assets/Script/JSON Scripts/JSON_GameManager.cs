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
    
    public JSON_GameStatus JgameStatus;
    string filePath;
    const string FILE_NAME = "DataStatus.json";

    public void Start()
    {

        filePath = Application.persistentDataPath;
        JgameStatus = new JSON_GameStatus();
        JgameStatus.playerPosition = new Vector3(16, 0, -45);
        Debug.Log(filePath);
        //startup initialisation
        LoadGameStatus();
    }

    public void LoadGameStatus()
    {
        if (File.Exists(filePath + "/" + FILE_NAME))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            JgameStatus = JsonUtility.FromJson<JSON_GameStatus>(loadedJson);
            GameObject.Find("Player").transform.position = JgameStatus.playerPosition;

        }
        else
        {
            resetGame();
        }
    }

    public void resetGame()
    {

        JgameStatus.spawnPoint = "Find the cat";//reference to a game object
        JgameStatus.health = 10;
        JgameStatus.Deaths = 0;
        JgameStatus.coinsCollected = 0;
        JgameStatus.playerPosition = new Vector3(16, 0, -45);
        JgameStatus.coinPosition = new Vector3(1, 1, 1);
        GameObject.Find("Player").transform.position = JgameStatus.playerPosition;

        SaveGameStatus();
    }


    public void SaveGameStatus()
    {
        string gameStatusJson = JsonUtility.ToJson(JgameStatus);

        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);

        JgameStatus.playerPosition = GameObject.Find("Player").transform.position;
    }


    public string UpdateStatus()
    {

        string message = "";
        message += "Task: " + JgameStatus.spawnPoint + "\n";
        message += "Health: " + JgameStatus.health + "\n";
        message += "Deaths: " + JgameStatus.Deaths + "\n";
        message += "Your HighScore: " + JgameStatus.coinsCollected + "\n";
        return message;


    }

}