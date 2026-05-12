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

    void Start()
    {
        gm = gameObject.GetComponentInChildren<JSON_GameManager>();

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


 

}
