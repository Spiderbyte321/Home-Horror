using System;
using UnityEngine;


public class LetterController : Interactable
{
    [SerializeField] private GameObject letterPrompt;
    [SerializeField] private CanvasGroup letterCanvasGroup;


    private void OnEnable()
    {
        ConfirmLetter.OnLetterConfirmedAction += HideLetter;
    }

    private void OnDisable()
    {
        ConfirmLetter.OnLetterConfirmedAction -= HideLetter;
    }

    private void OnTriggerEnter(Collider other)
    {
        letterPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        letterPrompt.SetActive(false);
    }

    public override void Interact()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        letterCanvasGroup.alpha = 1;
        letterCanvasGroup.blocksRaycasts = true;
        letterCanvasGroup.interactable = true;
    }


    private void HideLetter()
    {
        letterCanvasGroup.alpha = 0;
        letterCanvasGroup.blocksRaycasts = false;
        letterCanvasGroup.interactable = false;
    }
}
