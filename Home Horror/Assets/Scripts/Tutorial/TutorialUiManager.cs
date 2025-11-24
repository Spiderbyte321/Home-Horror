using System;
using TMPro;
using UnityEngine;

public class TutorialUiManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI PromptTextField;


   private void OnEnable()
   {
      TutorialContextualPrompt.PromptPlayerAction += DisplayContextualPrompt;
   }

   private void OnDisable()
   {
      TutorialContextualPrompt.PromptPlayerAction -= DisplayContextualPrompt;
   }

   private void DisplayContextualPrompt(string prompt)
   {
      PromptTextField.text = prompt;
   }
   
}
