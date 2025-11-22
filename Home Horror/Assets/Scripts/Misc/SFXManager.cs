using System;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("SFX Input")]
    [SerializeField] private string[] keys;
    [SerializeField] private AudioClip[] values;
    
    private Dictionary<string, AudioClip> SFXAudioClips = new Dictionary<string, AudioClip>();

    public static SFXManager instance;


    private void OnEnable()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            SFXAudioClips.Add(keys[i],values[i]);
        }
    }

    private void Start()
    {
        if(instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    public AudioClip GetAudio(string AudioKey)
    {
        return SFXAudioClips[AudioKey];
    }


    private void OnValidate()
    {
        if(values.Length != keys.Length)
        {
            Array.Resize(ref values,keys.Length);
        }
    }
}
