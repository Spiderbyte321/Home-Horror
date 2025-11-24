using System;
using UnityEngine;

public class TutorialContextualPrompt : MonoBehaviour
{
   [SerializeField] private string prompt;

   public delegate void OnPromptPlayer(string prompt);

   public static event OnPromptPlayer PromptPlayerAction;


   private void OnTriggerEnter(Collider other)
   {
      if(other.CompareTag("Player"))
         PromptPlayerAction?.Invoke(prompt);
   }
}
