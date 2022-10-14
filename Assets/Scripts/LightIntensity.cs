using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensity : MonoBehaviour
{
    [SerializeField] private Light2D light;
    void Start()
    {
        StartCoroutine(IntensityCoroutine());
    }
    IEnumerator IntensityCoroutine()
    {
        
        while (true)
        {
            if (light.intensity >= 1.1f) { light.intensity = 1f; }
            else if (light.intensity >= 1f) { light.intensity = 1.1f; }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
