using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.2f;
    public float speed = 8f;

    Light lightSource;
    float seed;
    //makes sure the light is connected to a source, and random makes the light flicker at random
    void Start()
    {
        lightSource = GetComponent<Light>();
        seed = Random.value * 100f;
    }
    //Makes the light flicker, thats what PerlinNoise is for it checks how much "noise" there is and in response
    //makes the light flicker 
    void Update()
    {
        float noise = Mathf.PerlinNoise(seed, Time.time * speed);
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}