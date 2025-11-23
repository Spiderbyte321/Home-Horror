using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    [Header("SFX Input")]
    [SerializeField] private string[] keys;
    [SerializeField] private AudioClip[] values;
    [SerializeField] private AudioSource globalSpeaker;
    [SerializeField] private AudioMixer sfxMixer;
    
    private Dictionary<string, AudioClip> SFXAudioClips = new Dictionary<string, AudioClip>();
    private Queue<AudioClip> sfxQueue = new Queue<AudioClip>();
        
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

    public void playGlobalAudio(string AudioKey)
    {
        globalSpeaker.clip = SFXAudioClips[AudioKey];
        globalSpeaker.Play();
    }

    public void playglobalJumpScare(string AudioKey)//Rework this to just play a random jumpscare sound
    {
        if (sfxQueue.Count > 0)
        {
            Debug.Log("Enquing");
            sfxQueue.Enqueue(SFXAudioClips[AudioKey]);
        }
        else
        {
            Debug.Log("starting routine");
            sfxQueue.Enqueue(SFXAudioClips[AudioKey]);
            StartCoroutine(AudioQueue());
        }
    }

    public void playDialogue(string AudioKey)
    {
        
    }


    private IEnumerator AudioQueue()
    {
        do
        {
            Debug.Log("Playing clip");
            AudioClip effect = sfxQueue.Dequeue();

            globalSpeaker.clip = effect;
            globalSpeaker.Play();
            yield return new WaitForSeconds(effect.length+0.01f);
        } while (sfxQueue.Count>0);
    }
    private void OnValidate()
    {
        if(values.Length != keys.Length)
        {
            Array.Resize(ref values,keys.Length);
        }
    }

    
}
