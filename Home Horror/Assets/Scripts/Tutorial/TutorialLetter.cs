using UnityEngine;

public class TutorialLetter : Interactable
{
    
    public override void Interact()
    {
        //RevealLetterOnCanvas
        gameObject.SetActive(false);
    }
}
