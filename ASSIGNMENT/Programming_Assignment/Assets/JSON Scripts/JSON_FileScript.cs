using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Diagnostics;





public class JSON_FileScript : MonoBehaviour
{

    public bool isPaused = false;
    public Button pauseButton;

    int numberCoins = 5;

    public Text gameStatusUI;
    public Text gameOverUI;


    public JSON_GameManager gm;


    void Awake()
    {
        
    }



    void Start()
    {
        UpdateSceneFromManager();

    }

   
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        //Debug.Log (gm.gameStatus.health);

        //check current health level to determine whether player must die!
        if (gm.JgameStatus.health <= 0)
        {

            // Update UI 
            gameOverUI.text = "You Lose!";
            gm.resetGame();

            //MonoBehaviour has a gameObject property for the current game object
            Destroy(gameObject);

            // Destroy remaining AIBalls
            GameObject[] remainingAIBalls = GameObject.FindGameObjectsWithTag("NPCBall");
            foreach (GameObject go in remainingAIBalls)
            {
                Destroy(go);
            }
        }

        if (gm.JgameStatus.coinsCollected >= numberCoins)
        {
            // Update gamneoverUI with text 
            gameOverUI.text = "You Win!";
            // Reset Gamemanager variuables
            gm.resetGame();

            //MonoBehaviour has a gameObject property for the current game object
            Destroy(gameObject);

            // Destroy remaining AIBalls
            GameObject[] remainingAIBalls = GameObject.FindGameObjectsWithTag("NPCBall");
            foreach (GameObject go in remainingAIBalls)
            {
                Destroy(go);
            }
        }

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
        gm.JgameStatus.playerPosition = GameObject.Find("Player").transform.position;

    }





    void UpdateSceneFromManager()
    {
        GameObject.Find("Player").transform.position = gm.JgameStatus.playerPosition;
    }

}
