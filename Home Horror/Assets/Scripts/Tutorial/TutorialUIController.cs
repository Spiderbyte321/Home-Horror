using System;
using UnityEngine;

public class TutorialUIController : GameUI
{
    private void OnEnable()
    {
        ConfirmLetter.OnLetterConfirmedAction += RevealLetter;
    }

    private void OnDisable()
    {
        ConfirmLetter.OnLetterConfirmedAction -= RevealLetter;
    }


    private void RevealLetter()
    {
        moneyText.enabled = true;
    }
}
