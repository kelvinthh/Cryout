using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(Light))]
 public class FlickeringFire : MonoBehaviour
{
    public float minIntensity;
    public float maxIntensity;
    private new Light light;
    float random;

    void Start()
    {
        light = GetComponent<Light>();
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
