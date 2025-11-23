using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityDrainController : MonoBehaviour
{
    [SerializeField] private float drainRate;
    [SerializeField] private float drainDelay;
    [SerializeField] private int drainAmount;
    private bool isActive;

    public delegate void SanityDrainAction(int drainAmount);

    public static event SanityDrainAction OnSanityDrain;
    private void OnTriggerEnter(Collider other)
    {
        if(isActive)
        {
          StopCoroutine(SanityDrain());  
        }
        else
        {
            StartCoroutine(SanityDrain());
        }
    }

    private IEnumerator SanityDrain()
    {
        yield return new WaitForSeconds(drainDelay);

        while (true)
        {
            yield return new WaitForSeconds(drainRate);
            OnSanityDrain?.Invoke(drainAmount);
        }
    }
}
