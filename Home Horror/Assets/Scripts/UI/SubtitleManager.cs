using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubtitleEntry
{
    public string soundName; 
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
    public List<SubtitleEntry> subtitleEntries = new List<SubtitleEntry>();
    
    private Dictionary<string, SubtitleEntry> subtitleDictionary = new Dictionary<string, SubtitleEntry>();

    void Start()
    {
        subtitleDisplay.text = "";
        
        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
        
        foreach (var entry in subtitleEntries)
        {
            if (!string.IsNullOrEmpty(entry.soundName) && entry.audioClip != null)
            {
                if (!subtitleDictionary.ContainsKey(entry.soundName))
                {
                    subtitleDictionary.Add(entry.soundName, entry);
                }
                else
                {
                    Debug.LogWarning($"Duplicate fileName key detected: {entry.soundName}");
                }
            }
        }
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
        if (subtitleDictionary.Count == 0) return;

        List<string> keys = new List<string>(subtitleDictionary.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];

        PlaySubtitle(randomKey);
    }

    public void PlaySubtitle(string fileName)
    {
        if (subtitleDictionary.TryGetValue(fileName, out SubtitleEntry entry))
        {
            StopAllCoroutines();
            StartCoroutine(ShowSubtitle(entry));
        }
        else
        {
            Debug.LogWarning($"Subtitle with fileName '{fileName}' not found!");
        }
    }

    IEnumerator ShowSubtitle(SubtitleEntry entry)
    {
        if (subtitlePanel != null)
            subtitlePanel.SetActive(true);

        audioSource.clip = entry.audioClip;
        audioSource.Play();

        subtitleDisplay.text = entry.subtitleText;

        yield return new WaitForSeconds(entry.audioClip.length);

        subtitleDisplay.text = "";
        
        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
    }
}
