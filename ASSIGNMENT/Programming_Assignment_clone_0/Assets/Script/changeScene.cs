using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;



public class changeScreen : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene ("GAME");

    }
    public void ChangeSceneMulti()
    {
        SceneManager.LoadScene("Multiplayer");

    }




}
