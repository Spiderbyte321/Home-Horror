using UnityEngine;


public class SFXController : MonoBehaviour
{
    [SerializeField] private string[] scareKeys;
    [SerializeField] private AudioSource audioSource;


    public void playSFX(string SFXKey)
    {
        audioSource.clip = SFXManager.instance.GetAudio(SFXKey);
        audioSource.Play();
    }

    public void playLoopedSFX(string SFXkey)
    {
        audioSource.clip = SFXManager.instance.GetAudio(SFXkey);
        audioSource.loop = true;
        audioSource.Play();
    }

    public void playJumpScare()
    {
        audioSource.clip = SFXManager.instance.GetAudio(scareKeys[Random.Range(0, scareKeys.Length)]);
        audioSource.Play();
    }

    public void stopSFX()
    {
        audioSource.Stop();
    }
}
