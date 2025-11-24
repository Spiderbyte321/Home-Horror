using System;
using UnityEngine;

public class TutorialCollisionBox : MonoBehaviour
{

    [SerializeField] private string VoiceKey;

    public delegate void OnTutorialBoxTriggered(TutorialCollisionBox thisBox);

    public static event OnTutorialBoxTriggered TutorialBoxTriggeredAction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             TutorialBoxTriggeredAction?.Invoke(this);
             SubtitleManager.instance.PlaySubtitle(VoiceKey);
             Destroy(this);
        }
           
            
    }
}
