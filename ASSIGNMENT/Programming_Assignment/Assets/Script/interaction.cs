using UnityEngine;
using UnityEngine.Events;

public class interaction : MonoBehaviour
{
    public UnityEvent enteredTrigger, exitedTrigger, interacted;

    private bool insideTrigger;

    // Update is called once per frame
    void Update()
    {
        if (insideTrigger)
        {
            interacted?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            enteredTrigger.Invoke();
            insideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            exitedTrigger.Invoke();
            insideTrigger = false;
        }
    }

}