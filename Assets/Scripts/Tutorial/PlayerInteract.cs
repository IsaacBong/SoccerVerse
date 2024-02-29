using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public IInteraction targetScript;

    //get a reference to an interactable you are to
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteraction>() != null)
        {
            targetScript = other.GetComponent<IInteraction>();
        }
    }
    //remove the reference to the interactable if you are exiting
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteraction>() == targetScript)
        {
            targetScript = null;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode.I) && targetScript != null)
        {
            targetScript.Interact();
        }
    }
}
