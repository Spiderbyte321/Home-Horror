using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [System.Serializable]
    public class FlickerLight
    {
        public Light light;
        public float flickerSpeed = 1f;
        public float minIntensity = 0.5f;
        public float maxIntensity = 1.5f;
    }

    public List<FlickerLight> lights = new List<FlickerLight>();
    private bool flickering = false;
    
    public void StartFlickering() => flickering = true;

    public void StopFlickering()
    {
        flickering = false;

        foreach (var flickerLight in lights)
        {
            if (flickerLight.light != null)
            {
                flickerLight.light.intensity = flickerLight.maxIntensity;
            }
        }
    }

    private void Update()
    {
        if (!flickering) return;

        foreach (var flickerLight in lights)
        {
            if (flickerLight.light != null)
            {
                float noise = Mathf.PerlinNoise(Time.time * flickerLight.flickerSpeed, 0.0f);
                flickerLight.light.intensity = Mathf.Lerp(flickerLight.minIntensity, flickerLight.maxIntensity, noise);
            }
        }
    }
}
