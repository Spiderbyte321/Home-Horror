using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlackout : MonoBehaviour
{
    public List<Light> lights;
    public float minBlackoutDuration = 1f;
    public float maxBlackoutDuration = 3f;
    public float intervalBetweenBlackouts = 5f;

    private bool isActive = false;

    public void StartRandomBlackouts()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(RandomBlackoutRoutine());
        }
    }

    public void StopRandomBlackouts()
    {
        isActive = false;
        StopAllCoroutines();
        SetLights(true); // Turn lights back on
    }

    private IEnumerator RandomBlackoutRoutine()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(intervalBetweenBlackouts);

            SetLights(false);
            float blackoutTime = Random.Range(minBlackoutDuration, maxBlackoutDuration);
            yield return new WaitForSeconds(blackoutTime);
            SetLights(true);
        }
    }

    private void SetLights(bool state)
    {
        foreach (var light in lights)
        {
            if (light != null) light.enabled = state;
        }
    }

}
