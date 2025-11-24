using System;
using System.Collections;
using UnityEngine;

public class BasementDoorPrompter : MonoBehaviour
{
    [SerializeField] private AudioSource speaker;
    private void OnEnable()
    {
        TutorialManager.PlayerAwokenAction += BangAudio;
    }

    private void OnDisable()
    {
        TutorialManager.PlayerAwokenAction -= BangAudio;
    }


    private void BangAudio()
    {
        speaker.Play();
        StartCoroutine(stopBanging());
    }

    private IEnumerator stopBanging()
    {
        yield return new WaitForSeconds(7);
        
        speaker.Stop();
    }
}
