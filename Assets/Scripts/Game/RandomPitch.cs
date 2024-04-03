using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    public float minPitch = 0.6f;
    public float maxPitch = 1.4f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Generate random pitch and volume
        float randomPitch = Random.Range(minPitch, maxPitch);

        // Apply pitch and volume
        audioSource.pitch = randomPitch;

    }
}