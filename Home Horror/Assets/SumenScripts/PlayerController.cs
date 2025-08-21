using System;
using JacobScripts.Pickupables;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Interactable InteractObject;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& InteractObject is not null)
        {
            InteractObject.Interact();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("running");
        other.gameObject.TryGetComponent(out InteractObject);
    }

    private void OnTriggerExit(Collider other)
    {
        InteractObject = null;
    }
}
