using UnityEngine;

public class ConfirmLetter : MonoBehaviour
{

    public delegate void LetterConfrimed();

    public static event LetterConfrimed OnLetterConfirmedAction;

    public void confirm()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnLetterConfirmedAction?.Invoke();
        SubtitleManager.instance.PlaySubtitle("freedom");
    }
}
