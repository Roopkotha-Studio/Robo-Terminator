using System.Collections;
using UnityEngine;

public class ThunderstormWeather : MonoBehaviour
{
    [SerializeField] private Vector2 thunderTime = new Vector2(15, 25);
    [SerializeField] private Vector2 flashTime = new Vector3(0.1f, 0.2f);
    [SerializeField] private new Light light = null;
    [SerializeField] private AudioClip[] thunderAudio = new AudioClip[0];

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(main());
    }

    IEnumerator main()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(thunderTime.x, thunderTime.y));
            StartCoroutine(thunderFlash());
        }
    }

    IEnumerator thunderFlash()
    {
        float intensity = light.intensity;
        float shadowStrength = light.shadowStrength;
        light.intensity = 666;
        light.shadowStrength = 0;
        if (thunderAudio.Length > 0)
        {
            audioSource.PlayOneShot(thunderAudio[Random.Range(0, thunderAudio.Length - 1)]);
        } else
        {
            audioSource.Play();
        }
        yield return new WaitForSeconds(Random.Range(flashTime.x, flashTime.y));
        light.intensity = intensity;
        light.shadowStrength = shadowStrength;
    }
}