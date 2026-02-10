using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class Scene1_Script3 : MonoBehaviour
{
      //declare our state variables as simple types for now
    string playerName;
      int highScore;
      int currentLevel;
      //reference to a game object
      string spawnPoint;
      int health;
      int coins;
      //reference to UI Text Object
      TMP_Text scoreUI;
      //setup properties for all of our state variables
      public string Name
      {
            get {return playerName;}
            //the value keyword specifies the setter value passed in
            set {playerName = value;}
      }
      public int Score
      {
            get {return highScore;}
            //the value keyword specifies the setter value passed in
            set {highScore = value;}
      }
      public int Level
      {
            get {return currentLevel;}
            //the value keyword specifies the setter value passed in
            set {currentLevel = value;}
      }
      public string Spawn
      {
            get {return spawnPoint;}
            //the value keyword specifies the setter value passed in
            set {spawnPoint = value;}
      }
      public int HealthLevel
      {
            get {return health;}
            //the value keyword specifies the setter value passed in
            set {health = value;}
      }
      public int Economy
      {
            get {return coins;}
            //the value keyword specifies the setter value passed in
            set {coins = value;}
      }

      // Setup the initial game status
      void Start ()
      {
            // Assume first run, so state data is initialised
            playerName = "FAYEAZER";
            highScore = 0;
            currentLevel = 1;
            spawnPoint = "beginning";//reference to a game object
            health = 100;
            coins = 0;
            scoreUI = GetComponent<TMP_Text> ();
      }
      
      // Use the Update to refresh status on screen
      void Update ()
      {
            string scoreString = "playerName: " + playerName + "\n";
            scoreString += "highScore: " + highScore.ToString () + "\n";
            scoreString += "currentLevel: " + currentLevel.ToString () + "\n";
            scoreString += "spawnPoint: " + spawnPoint + "\n";
            scoreString += "health: " + health + "\n";
            scoreString += "coins: " + coins.ToString () + "\n";
            scoreUI.text = scoreString;
      }
}