using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("UI")]
    [SerializeField] private Canvas gameHUD = null;
    [SerializeField] private Canvas gamePausedMenu = null;
    [SerializeField] private Canvas gameOverMenu = null;
    [SerializeField] private Canvas levelCompletedMenu = null;
    [SerializeField] private Canvas settingsMenu = null;
    [SerializeField] private Canvas graphicsQualityMenu = null;
    [SerializeField] private Canvas soundMenu = null;
    [SerializeField] private Canvas quitGameMenu = null;
    [SerializeField] private Text enemiesLeftText = null;
    [SerializeField] private Text levelNameText = null;
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider loadingSlider = null;
    [SerializeField] private Text loadingPercentage = null;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip buttonClick = null;

    [Header("Setup")]
    [SerializeField] private string levelName = "Creaky Forest";
    [SerializeField] private AudioMixer audioMixer = null;

    private AudioSource audioSource;
    private Controls input;
    private long maxEnemies = 0;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public bool won = false;
    [HideInInspector] public bool paused = false;
    private int clickSource = 1; //1 is game paused menu, 2 is game over menu, 3 is level completed menu
    private bool loading = false;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        input = new Controls();
        if (audioSource) audioSource.ignoreListenerPause = true;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy && enemy.GetComponent<EnemyController>()) ++maxEnemies;
        }
        gameOver = false;
        won = false;
        paused = false;
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
        gameHUD.enabled = true;
        gamePausedMenu.enabled = false;
        gameOverMenu.enabled = false;
        levelCompletedMenu.enabled = false;
        settingsMenu.enabled = false;
        graphicsQualityMenu.enabled = false;
        soundMenu.enabled = false;
        quitGameMenu.enabled = false;
    }

    void OnEnable()
    {
        input.Enable();
        input.Gameplay.Pause.performed += context => pause();
        input.Gameplay.Resume.performed += context => resume(false);
        input.Gameplay.CloseMenu.performed += context => closeMenu();
    }

    void OnDisable()
    {
        input.Disable();
        input.Gameplay.Pause.performed -= context => pause();
        input.Gameplay.Resume.performed -= context => resume(false);
        input.Gameplay.CloseMenu.performed -= context => closeMenu();
    }

    void Update()
    {
        long enemies = 0;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy && enemy.GetComponent<EnemyController>() && !enemy.GetComponent<EnemyHealth>().dead) ++enemies;
        }
        if (gameOver)
        {
            clickSource = 2;
            if (!loading && !settingsMenu.enabled && !graphicsQualityMenu.enabled && !soundMenu.enabled && !quitGameMenu.enabled) gameOverMenu.enabled = true;
        }
        if (!gameOver && !won && enemies <= 0)
        {
            won = true;
            if (PlayerPrefs.GetInt("Level") < PlayerPrefs.GetInt("MaxLevels"))
            {
                if (!loading && !settingsMenu.enabled && !graphicsQualityMenu.enabled && !soundMenu.enabled && !quitGameMenu.enabled) levelCompletedMenu.enabled = true;
                if (!PlayerPrefs.HasKey("Restarted"))
                {
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                    PlayerPrefs.Save();
                }
            } else
            {
                if (!loading) StartCoroutine(loadScene("Ending"));
            }
        }
        enemiesLeftText.text = "Enemies: " + enemies + " / " + maxEnemies;
        levelNameText.text = levelName;
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

    #region Input Functions
    void pause()
    {
        if (!gameOver && !won && !gameOverMenu.enabled && !levelCompletedMenu.enabled)
        {
            if (!paused) //Pauses the game
            {
                clickSource = 1;
                paused = true;
                Time.timeScale = 0;
                AudioListener.pause = true;
                gamePausedMenu.enabled = true;
            } else //Unpauses the game
            {
                if (!settingsMenu.enabled && !graphicsQualityMenu.enabled && !soundMenu.enabled && !quitGameMenu.enabled)
                {
                    paused = false;
                    Time.timeScale = 1;
                    AudioListener.pause = false;
                    gamePausedMenu.enabled = false;
                }
            }
        }
    }

    void closeMenu()
    {
        if (paused)
        {
            if (settingsMenu.enabled)
            {
                settingsMenu.enabled = false;
                if (clickSource <= 1)
                {
                    gamePausedMenu.enabled = true;
                } else if (clickSource == 2)
                {
                    gameOverMenu.enabled = true;
                } else if (clickSource >= 3)
                {
                    levelCompletedMenu.enabled = true;
                }
            } else if (graphicsQualityMenu.enabled)
            {
                graphicsQualityMenu.enabled = false;
                settingsMenu.enabled = true;
            } else if (soundMenu.enabled)
            {
                soundMenu.enabled = false;
                settingsMenu.enabled = true;
            } else if (quitGameMenu.enabled)
            {
                quitGameMenu.enabled = false;
                if (clickSource <= 1)
                {
                    gamePausedMenu.enabled = true;
                } else if (clickSource == 2)
                {
                    gameOverMenu.enabled = true;
                } else if (clickSource >= 3)
                {
                    levelCompletedMenu.enabled = true;
                }
            }
        }
    }
    #endregion

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
                    load.allowSceneActivation = false;
                    loadingSlider.value = load.progress;
                    loadingPercentage.text = Mathf.Floor(load.progress * 100) + "%";
                } else
                {
                    if (PlayerPrefs.GetInt("Tips") >= 1)
                    {
                        if (Input.anyKeyDown) load.allowSceneActivation = true;
                        loadingSlider.value = 1;
                        loadingPercentage.text = "100%";
                    } else
                    {
                        load.allowSceneActivation = true;
                        loadingSlider.value = 1;
                        loadingPercentage.text = "100%";
                    }
                }
                gameHUD.enabled = false;
                gamePausedMenu.enabled = false;
                gameOverMenu.enabled = false;
                levelCompletedMenu.enabled = false;
                settingsMenu.enabled = false;
                graphicsQualityMenu.enabled = false;
                soundMenu.enabled = false;
                quitGameMenu.enabled = false;
                yield return null;
            }
        }
    }

    #region Menu Functions
    public void resume(bool wasClicked)
    {
        if (!settingsMenu.enabled && !graphicsQualityMenu.enabled && !soundMenu.enabled)
        {
            if (audioSource && wasClicked)
            {
                if (buttonClick)
                {
                    audioSource.PlayOneShot(buttonClick);
                } else
                {
                    audioSource.Play();
                }
            }
            paused = false;
            Time.timeScale = 1;
            AudioListener.pause = false;
            gamePausedMenu.enabled = false;
        }
    }

    public void toNextLevel()
    {
        if (won && levelCompletedMenu.enabled)
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
            if (PlayerPrefs.GetInt("Level") < PlayerPrefs.GetInt("MaxLevels"))
            {
                StartCoroutine(loadScene("Level " + PlayerPrefs.GetInt("Level")));
            } else
            {
                StartCoroutine(loadScene("Ending"));
            }
        }
    }

    public void restart(bool wasClicked)
    {
        if (audioSource && wasClicked)
        {
            if (buttonClick)
            {
                audioSource.PlayOneShot(buttonClick);
            } else
            {
                audioSource.Play();
            }
        }
        StartCoroutine(loadScene(SceneManager.GetActiveScene().name));
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

    public void exitToMainMenu()
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
        StartCoroutine(loadScene("Main Menu"));
    }

    public void openCanvasFromClickSource(Canvas canvas)
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
            if (clickSource <= 1)
            {
                gamePausedMenu.enabled = false;
            } else if (clickSource == 2)
            {
                gameOverMenu.enabled = false;
            } else if (clickSource >= 3)
            {
                levelCompletedMenu.enabled = false;
            }
        } else
        {
            canvas.enabled = false;
            if (clickSource <= 1)
            {
                gamePausedMenu.enabled = true;
            } else if (clickSource == 2)
            {
                gameOverMenu.enabled = true;
            } else if (clickSource >= 3)
            {
                levelCompletedMenu.enabled = true;
            }
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
    #endregion
}