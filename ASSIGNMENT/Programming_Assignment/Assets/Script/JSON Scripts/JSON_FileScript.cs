using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Diagnostics;





public class JSON_FileScript : MonoBehaviour
{
    int numberCoins = 5;

    public Text gameStatusUI;
    public Text gameOverUI;

    public bool isPaused = false;
    public Button pauseButton;


    public JSON_GameManager gm;


    void Awake()
    {
        
    }



    void Start()
    {
        gm = gameObject.GetComponentInChildren<JSON_GameManager>();
        gm.Start();


        UpdateSceneFromManager();


    }

   
    void Update()
    {
        //gm.JgameStatus.playerPosition = GameObject.Find("Player").transform.position;
    }

    private void FixedUpdate()
    {

        gameStatusUI.text = gm.UpdateStatus();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Coin")
        {
      
            Destroy(col.gameObject);
            gm.JgameStatus.coinsCollected += 1;
        }


        if (col.gameObject.name == "Enemy")
        {
            gm.JgameStatus.health -= 1;
        }

        if (col.gameObject.name == "Checkpoint1")
        {
            gm.JgameStatus.spawnPoint = "Home";
        }
    }

 

    public void OnApplicationPause(bool pauseStatus)
    {

        if (gm != null)
        {
            // if Game is paused, savegame
            if (pauseStatus)
            {
                // Save Game data
                //gm.SaveGameStatus ();
            }
            else
            {
                // Load Game data
                gm.LoadGameStatus();
            }
        }

        //Debug.Log("OnApplicationPause Called");
    }

    void OnApplicationQuit()
    {

        // Save Scene Data to the GameManager 
        SaveFromSceneToManager();

        //Debug.Log("OnApplicationQuit Called");
    }

    // Save data from the scene to the manager
    void SaveFromSceneToManager()
    {

        // Empty the GameManager NPCBalls Array so that there is always
        // the correct number of balls after some have been destroyed
       

        // Update Player Position in the GameManager with the position of the Player in the scene
        // This will be stored on the JSON file when the application quits 
        //gm.JgameStatus.playerPosition = GameObject.Find("Player").transform.position;

    }


    public void PauseGame()
    {
        if (!isPaused) {

            Time.timeScale = 0;
            //Debug.Break();
            isPaused = true;
            GameObject.Find("Pause").GetComponentInChildren<Text>().text = "Resume";
        }
        else
        {
            Time.timeScale = 1;
            //Debug.Break();
            isPaused = false;
            GameObject.Find("Pause").GetComponentInChildren<Text>().text = "Pause";
        }

    }


    void UpdateSceneFromManager()
    {
        //GameObject.Find("Player").transform.position = gm.JgameStatus.playerPosition;
    }

}
