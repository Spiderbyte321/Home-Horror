using System;
using UnityEngine;

public class ContextualDialogue : MonoBehaviour
{
    [SerializeField] private string dialogueKey;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             PlayDialogue();
             Destroy(this);
        }
           
    }

    private void PlayDialogue()
    {
        SubtitleManager.instance.PlaySubtitle(dialogueKey);
    }
}
