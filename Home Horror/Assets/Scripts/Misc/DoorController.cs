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
        togglePromptVisibility();   
    }

    private void OnTriggerExit(Collider other)
    {
        togglePromptVisibility();
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

    private void toggleDoorState()
    {
        Debug.Log("toggling door state");
        renderer.enabled = !renderer.enabled;
         collisionBox.enabled = !collisionBox.enabled;
    }
}
