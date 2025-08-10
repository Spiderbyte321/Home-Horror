using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubtitleLine
{
    public AudioClip audioClip;
    [TextArea] public string subtitleText;
}

public class SubtitleManager : MonoBehaviour
{
    public TMP_Text subtitleDisplay;
    public GameObject subtitlePanel; 
    public KeyCode triggerKey = KeyCode.Space;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Subtitles")]
    public List<SubtitleLine> subtitleLines = new List<SubtitleLine>();



    void Start()
    {
        subtitleDisplay.text = "";
        if (subtitlePanel != null)
            subtitlePanel.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            PlayRandomSubtitle();
        }
    }

    void PlayRandomSubtitle()
    {
        if (subtitleLines.Count == 0) return;

        int randomIndex = Random.Range(0, subtitleLines.Count);
        StopAllCoroutines();
        StartCoroutine(ShowSubtitle(subtitleLines[randomIndex]));
    }

    IEnumerator ShowSubtitle(SubtitleLine line)
    {
        if (subtitlePanel != null)
            subtitlePanel.SetActive(true);

        audioSource.clip = line.audioClip;
        audioSource.Play();

        subtitleDisplay.text = line.subtitleText;

        yield return new WaitForSeconds(line.audioClip.length);

        subtitleDisplay.text = "";
        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
    }
}
