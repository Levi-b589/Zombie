using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.2f;
    public float speed = 8f;

    Light lightSource;
    float seed;

    void Start()
    {
        lightSource = GetComponent<Light>();
        seed = Random.value * 100f;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(seed, Time.time * speed);
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}