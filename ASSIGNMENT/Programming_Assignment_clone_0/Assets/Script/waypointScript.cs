using UnityEngine;
using System.Collections;

public class waypointScript : MonoBehaviour
{
    int nextIndex;
    public GameObject[] waypoints;

    public GameObject NextWaypoint(GameObject current)
    {
        if (current != null)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {

                if (current == waypoints[i])
                {
                    nextIndex = (i + 1) % waypoints.Length;
                }
            }
        }
        else
        {
            nextIndex = 0;
        }

        return waypoints[nextIndex];
    }
}

