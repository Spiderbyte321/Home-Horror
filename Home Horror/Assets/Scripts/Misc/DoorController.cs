using System;
using UnityEngine;

public class DoorController : Interactable
{
    [SerializeField] private GameObject prompter;
    [SerializeField] private GameObject secondPrompter;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Collider collisionBox;
    private bool isPrompterActive;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            togglePromptVisibility();
        
        if(other.CompareTag("Enemy"))
            toggleDoorState();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            togglePromptVisibility();
        
        if(other.CompareTag("Enemy"))
            toggleDoorState();
    }

    private void togglePromptVisibility()
    {
        isPrompterActive = !isPrompterActive;
        prompter.SetActive(isPrompterActive);
        secondPrompter.SetActive(isPrompterActive);
    }


    public override void Interact()
    {
        toggleDoorState();
    }

    protected virtual void toggleDoorState()
    {
        renderer.enabled = !renderer.enabled;
         collisionBox.enabled = !collisionBox.enabled;
    }
}
