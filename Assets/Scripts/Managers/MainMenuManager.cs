using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Credits Settings")]
    [Tooltip("The Y position credits start at.")] [SerializeField] private float creditsY = 450;
    [SerializeField] private float creditsScrollSpeed = 0.5f;
    [SerializeField] private float creditsFastSpeed = 1;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip buttonClick = null;

    [Header("Setup")]
    [SerializeField] private Canvas mainMenu = null;
    [SerializeField] private Canvas settingsMenu = null;
    [SerializeField] private Canvas graphicsQualityMenu = null;
    [SerializeField] private Canvas soundMenu = null;
    [SerializeField] private Canvas creditsMenu = null;
    [SerializeField] private RectTransform credits = null;
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider loadingSlider = null;
    [SerializeField] private Text loadingPercentage = null;
    [SerializeField] private AudioMixer audioMixer = null;

    private AudioSource audioSource;
    private Controls input;
    private bool fastCredits = false;
    private bool loading = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        input = new Controls();
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
        creditsMenu.enabled = false;
    }

    void OnEnable()
    {
        input.Enable();
        input.Gameplay.CloseMenu.performed += context => closeMenu();
        input.Menu.SpeedUpCredits.performed += context => speedUpCredits(true);
        input.Menu.SpeedUpCredits.canceled += context => speedUpCredits(false);
    }

    void OnDisable()
    {
        input.Disable();
        input.Gameplay.CloseMenu.performed -= context => closeMenu();
        input.Menu.SpeedUpCredits.performed -= context => speedUpCredits(true);
        input.Menu.SpeedUpCredits.canceled -= context => speedUpCredits(false);
    }

    void Update()
    {
        if (!creditsMenu.enabled) credits.anchoredPosition = new Vector2(0, creditsY);
        if (!loading)
        {
            loadingScreen.SetActive(false);
        } else
        {
            loadingScreen.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level") < 1)
        {
            PlayerPrefs.SetInt("Level", 1);
        } else if (PlayerPrefs.GetInt("Level") > PlayerPrefs.GetInt("MaxLevels"))
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("MaxLevels"));
        }
    }

    #region Main Functions
    IEnumerator scrollCredits()
    {
        while (creditsMenu.enabled)
        {
            yield return new WaitForEndOfFrame();
            float speed = creditsScrollSpeed;
            if (!fastCredits)
            {
                speed = creditsScrollSpeed;
            } else
            {
                speed = creditsFastSpeed;
            }
            if (creditsMenu.enabled) credits.anchoredPosition -= new Vector2(0, speed);
            if (credits.anchoredPosition.y <= -creditsY)
            {
                mainMenu.enabled = true;
                creditsMenu.enabled = false;
                yield break;
            }
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
                creditsMenu.enabled = false;
                yield return null;
            }
        }
    }
    #endregion

    #region Input Functions
    void closeMenu()
    {
        if (settingsMenu.enabled)
        {
            settingsMenu.enabled = false;
            mainMenu.enabled = true;
        } else if (graphicsQualityMenu.enabled)
        {
            graphicsQualityMenu.enabled = false;
            settingsMenu.enabled = true;
        } else if (soundMenu.enabled)
        {
            soundMenu.enabled = false;
            settingsMenu.enabled = true;
        } else if (creditsMenu.enabled)
        {
            creditsMenu.enabled = false;
            mainMenu.enabled = true;
            StopCoroutine(scrollCredits());
        }
    }

    void speedUpCredits(bool state)
    {
        fastCredits = state;
    }
    #endregion

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
        if (PlayerPrefs.GetInt("Level") > 0)
        {
            StartCoroutine(loadScene("Level " + level));
        } else
        {
            StartCoroutine(loadScene("Level 1"));
        }
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

    public void openCredits()
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
        if (!creditsMenu.enabled)
        {
            creditsMenu.enabled = true;
            mainMenu.enabled = false;
            StartCoroutine(scrollCredits());
        } else
        {
            creditsMenu.enabled = false;
            mainMenu.enabled = true;
            StopCoroutine(scrollCredits());
        }
    }

    public void exitGame()
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