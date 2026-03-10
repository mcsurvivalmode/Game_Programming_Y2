using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Scene1_Script1 : MonoBehaviour
{

	//declare a public variable to reference Canvas/score TextUI
	public Text gameStatusUI;
	public Text gameOverUI;
	//now input control axes
	float verticalMovement;
	float horizontalMovement;

    // Variable for Pause Test
    public bool isPaused = false;
    public Button pauseButton;

	//number of coins to create
	int numberCoins = 20;

	// Declare GameManager Object
	public MB_GameManager gm;

	void Start(){

		// Instantiate MB GameManager Object
		gm = gameObject.GetComponentInChildren<MB_GameManager>();
		gm.Start ();

		// Randomly position number of remaining coins (numberCoins-numbercoinscollected) on a 20x20 grid
		for (int i = 0; i < numberCoins - gm.gameStatus.coinsCollected; i++) {
			// Create 20 coins on a 20x20 grid
			Instantiate(Resources.Load("Coin"),new Vector3(Random.Range (-24.0f, 24.0f), 1.0f, Random.Range (-24.0f, 24.0f)), Quaternion.identity);
		}

		// Update the Scene with Data from Game Manager
		UpdateSceneFromManager ();
	}

	// OnCollisionEnter will trigger when a collision begins
	void OnCollisionEnter (Collision col)
	{
			// hitting Spheres is bad for our health!!
		if (col.gameObject.name == "NPCBall(Clone)") {
			Destroy (col.gameObject);
			//reduce health level
			gm.gameStatus.health -= 25;
		}
	}//end of collision condition

	// OnTriggerEnter will trigger when a Coins Collected
	void OnTriggerEnter (Collider col)
	{
		// The collision will return the gameObject itself- the name property allows different
		// hitting a Coin benefits the economy!
		if (col.gameObject.name == "Coin(Clone)") {
			// Destroy Coin
			Destroy (col.gameObject);
			//now update the state data
			gm.gameStatus.coinsCollected += 1;
		}//end of collision condition
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//Debug.Log (gm.gameStatus.health);

		//check current health level to determine whether player must die!
		if (gm.gameStatus.health <= 0) {
			
			// Update UI 
			gameOverUI.text = "You Lose!";
			gm.resetGame ();

			//MonoBehaviour has a gameObject property for the current game object
			Destroy (gameObject);

			// Destroy remaining AIBalls
			GameObject[] remainingAIBalls = GameObject.FindGameObjectsWithTag("NPCBall");
			foreach (GameObject go in remainingAIBalls) {
				Destroy(go);
			}
		}

		if(gm.gameStatus.coinsCollected >= numberCoins){
			// Update gamneoverUI with text 
			gameOverUI.text = "You Win!";
			// Reset Gamemanager variuables
			gm.resetGame ();

			//MonoBehaviour has a gameObject property for the current game object
			Destroy (gameObject);

			// Destroy remaining AIBalls
			GameObject[] remainingAIBalls = GameObject.FindGameObjectsWithTag("NPCBall");
			foreach (GameObject go in remainingAIBalls) {
				Destroy(go);
			}
		} 

		gameStatusUI.text = gm.UpdateStatus ();
		Debug.Log(gameStatusUI.text);

		if (gameObject != null) {

			//get the input values for the horizontal and vertical axes
			verticalMovement = Input.GetAxis ("Vertical");
			horizontalMovement = Input.GetAxis ("Horizontal");
			//now a compound if statement to determine the direction of the vector
			Vector3 myDirectionVector = new Vector3 ();
			if (verticalMovement > 0) {
				myDirectionVector = Vector3.forward * verticalMovement;
			} else if (verticalMovement < 0) {
				myDirectionVector = Vector3.back * -verticalMovement;
			} else if (horizontalMovement > 0) {
				myDirectionVector = Vector3.right * horizontalMovement;
			} else if (horizontalMovement < 0) {
				myDirectionVector = Vector3.left * -horizontalMovement;
			}
			//add force to the sphere to move it- 
			GetComponent<Rigidbody> ().AddForce (myDirectionVector / 5, ForceMode.Impulse);
		}
	}

	void OnApplicationPause(bool pauseStatus) {

		if (gm!=null) {
			// if Game is paused, savegame
			if (pauseStatus) {
				// Save Game data
				gm.SaveGameStatus ();
			} else {
				// Load Game data
				gm.LoadGameStatus ();
			}
		}

		Debug.Log ("OnApplicationPause Called");
	}

	void OnApplicationQuit() {

		// Save Scene Data to the GameManager 
		SaveFromSceneToManager ();

		// Save Game Status (HUD)
		gm.SaveGameStatus ();

		Debug.Log ("OnApplicationQuit Called");
	}

	// Save data from the scene to the manager
	void SaveFromSceneToManager(){

		// Empty the GameManager NPCBalls Array so that there is always
		// the correct number of balls after some have been destroyed
		gm.gameStatus.NPCs.Clear();
	
		// Update Player Position in the GameManager with the position of the Player in the scene
		// This will be stored on the JSON file when the application quits 
		gm.gameStatus.playerPosition = GameObject.Find("Player").transform.position;

		// Get position of all NPCBalls and put them in an array
		GameObject[] npcBalls = GameObject.FindGameObjectsWithTag ("NPCBall");

		// Loop through NPCBalls and update the GameManagers NPCPOsitions array with
		// the values from the NPCBalls in the scene, this keep an update of the positions
		// at each frame. This will be stored on the JSON file when the application quits 
		for (int i = 0; i < npcBalls.Length; i++) {

			// Update each ball position in the Game Manager with the vale from the scene
			gm.gameStatus.NPCs.Add (npcBalls [i].transform.position);
		} 
	}

	void UpdateSceneFromManager(){
	
		// Update position of Player in Game Manager
		GameObject.Find("Player").transform.position = gm.gameStatus.playerPosition;

		// Alternative
		// Create Player object from position of Player in Game Manager
		// GameObject player = Instantiate (Resources.Load ("Player"), gm.gameStatus.playerPosition, Quaternion.identity) as GameObject;

		// Get position of all NPCBalls and put them in an array
		List<GameObject> npcBalls = new List<GameObject>();
		 
		foreach (Vector3 npcPosition in gm.gameStatus.NPCs) {
			// Create each NPC from the the NPCBall Prefab. A reference to the PlayerBall must also be made next
			GameObject npc = Instantiate (Resources.Load ("NPCBall"), npcPosition, Quaternion.identity) as GameObject;
			// Make referenvce to the player object. This is needed so that the NPC can follow the Player
			npc.GetComponent<Scene1_Script2> ().playerSphere = GameObject.Find ("Player");
			// Add NPC to the NPC List for reference purposes
			npcBalls.Add (npc);
		}
	}

    // Example of pausing game from code!
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
