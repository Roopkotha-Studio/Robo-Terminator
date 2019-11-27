using UnityEngine;

public class QualityChanger : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClick = null;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource) audioSource.ignoreListenerPause = true;
    }

    public void changeQuality(int quality)
    {
        if (audioSource)
        {
            if (buttonClick)
            {
                audioSource.PlayOneShot(buttonClick);
            } else
            {
                audioSource.Play();
            }
        }
        QualitySettings.SetQualityLevel(quality);
    }
}
