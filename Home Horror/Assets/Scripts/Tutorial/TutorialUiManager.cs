using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialUiManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI PromptTextField;
   [SerializeField] private ScreenFader fader;


   private void OnEnable()
   {
      TutorialContextualPrompt.PromptPlayerAction += DisplayContextualPrompt;
      DayEndController.OnBedInteracted += FadeScreen;
   }

   private void OnDisable()
   {
      TutorialContextualPrompt.PromptPlayerAction -= DisplayContextualPrompt;
      DayEndController.OnBedInteracted -= FadeScreen;
   }

   private void DisplayContextualPrompt(string prompt)
   {
      PromptTextField.text = prompt;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.F))
      {
         PromptTextField.text = "";
      }
   }

   private void FadeScreen()
   {
      StartCoroutine(fader.FadeOut());
      StartCoroutine(fadeBackIn());
   }

   private IEnumerator fadeBackIn()
   {
      yield return new WaitForSeconds(3);
      StartCoroutine(fader.FadeIn());
   }
}
