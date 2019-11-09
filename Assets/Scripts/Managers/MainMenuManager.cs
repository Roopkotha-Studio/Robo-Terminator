using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioClip buttonClick = null;

    [Header("Setup")]
    [SerializeField] private Canvas mainMenu = null;
    [SerializeField] private Canvas settingsMenu = null;
    [SerializeField] private Canvas graphicsQualityMenu = null;
    [SerializeField] private Canvas soundMenu = null;
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider loadingSlider = null;
    [SerializeField] private Text loadingPercentage = null;
    [SerializeField] private AudioMixer audioMixer = null;

    private AudioSource audioSource;
    private bool loading = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource) audioSource.ignoreListenerPause = true;
        loading = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 1);
            PlayerPrefs.Save();
        } else
        {
            audioMixer.SetFloat("SoundVolume", Mathf.Log10(PlayerPrefs.GetFloat("SoundVolume")) * 20);
        }
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.Save();
        } else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        }
        mainMenu.enabled = true;
        settingsMenu.enabled = false;
        graphicsQualityMenu.enabled = false;
        soundMenu.enabled = false;
    }

    void Update()
    {
        if (!loading)
        {
            loadingScreen.SetActive(false);
        } else
        {
            loadingScreen.SetActive(true);
        }
    }

    IEnumerator loadScene(string scene)
    {
        if (!loading)
        {
            loading = true;
            AsyncOperation load = SceneManager.LoadSceneAsync(scene);
            if (Camera.main.GetComponent<AudioSource>()) Camera.main.GetComponent<AudioSource>().Stop();
            while (!load.isDone)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                if (load.progress < 0.9f)
                {
                    loadingSlider.value = load.progress;
                    loadingPercentage.text = Mathf.Floor(load.progress * 100) + "%";
                } else
                {
                    loadingSlider.value = 1;
                    loadingPercentage.text = "100%";
                }
                mainMenu.enabled = false;
                settingsMenu.enabled = false;
                graphicsQualityMenu.enabled = false;
                soundMenu.enabled = false;
                yield return null;
            }
        }
    }

    #region Menu Functions
    public void startGame(int level)
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
        StartCoroutine(loadScene("Level " + level));
    }

    public void openCanvasFromMainMenu(Canvas canvas)
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
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            mainMenu.enabled = false;
        } else
        {
            canvas.enabled = false;
            mainMenu.enabled = true;
        }
    }

    public void openCanvasFromSettings(Canvas canvas)
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
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            settingsMenu.enabled = false;
        } else
        {
            canvas.enabled = false;
            settingsMenu.enabled = true;
        }
    }

    public void quitGame()
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
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion
}