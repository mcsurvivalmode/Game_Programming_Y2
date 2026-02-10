using System.Numerics;
using UnityEngine;

public class scene1_script1 : MonoBehaviour
{
   public GameObject textUI;
   //input controls
   float verticalMovement;
   float horizontalMovement;

   //on collision
   void onCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "Wall")
        {
            col.gameObject.GetComponent<Renderer> ().material.color = Color.Green;
            
        }
        else
        {
            if (col.gameObject.name == "enemy")
            {
                Destroy (col.gameObject);
                textUI.GetComponent<scene1_script3> ().HealthLevel -= 25;
            }
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (textUI.GetComponent<scene1_script3> ().HealthLevel <= 0)
        {
            Destroy (gameObject);
        } 
        else 
        {
            verticalMovement = Input.GetAxis ("Vertical");
            horizontalMovement = Input.GetAxis ("Horizontal");

            Vector3 myDirectionVector = new Vector3 ();
            if (verticalMMovement > 0)
            {
                myDirectionVector = Vector3.forward * verticalMovement;
            }
            else if (verticalMovement < 0)
            {
                myDirectionVector = Vector3.back * -verticalMovement;
            }
            else if (horizontalMovement > 0)
            {
                myDirectionVector = Vector3.right * horizontalMovement;
            }
            else if (horizontalMovement < 0)
            {
                myDirectionVector = Vector3.left*-horizontalMovement;
            }
            GetComponent<Rigidbody>().AddForce (myDirectionVector/5, ForceMode.Impulse);
        }
    }
}
