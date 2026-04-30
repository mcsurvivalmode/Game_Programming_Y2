using UnityEngine;

public class pointMovement : MonoBehaviour
{
   
    Vector3 source;
    Vector3 target;
    float speed;
    float distanceToTarget;
 
    const float DECELERATION_FACTOR = 0.6f;
    void FixedUpdate()
    {
        source = transform.position;
       
        if (Input.GetMouseButtonDown(0)) //mouse click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //is the screen clicked
            if (Physics.Raycast(ray, out hit))
            {
                
                target = hit.point;
            }
        }
        
        this.transform.position = FollowPoint(source, target);
    }

    private Vector3 FollowPoint(Vector3 source, Vector3 target) //move where the mouse clicked 
    {
        
        distanceToTarget = Vector3.Distance(source, target);
        speed = distanceToTarget / DECELERATION_FACTOR;
        return Vector3.MoveTowards(source, target, Time.deltaTime * speed);
    }
}
