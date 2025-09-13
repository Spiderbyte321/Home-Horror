using System.Collections.Generic;
using UnityEngine;

public class FullBlackout : MonoBehaviour
{
    public List<Light> allLights;

    public void TriggerFullBlackout()
    {
        foreach (var light in allLights)
        {
            if (light != null) light.enabled = false;
        }
    }
    
    public void RestoreLights()
    {
        foreach (var light in allLights)
        {
            if (light != null) light.enabled = true;
        }
    }
}
