using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("UI")]
    [SerializeField] private Text enemiesLeftText = null;
    [SerializeField] private Text mainText = null;

    [Header("Miscellanous")]
    [SerializeField] private int level = 1;
    public bool gameOver = false;
    public bool won = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
        gameOver = false;
        won = false;
    }

    void Update()
    {
        long enemies = 0;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy && enemy.GetComponent<EnemyController>()) ++enemies;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOver && !won)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            } else if (!gameOver && won)
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                PlayerPrefs.Save();
                SceneManager.LoadSceneAsync("Level " + PlayerPrefs.GetInt("Level"));
            }
        }
        if (gameOver) mainText.text = "GAME OVER";
        if (!won && enemies <= 0)
        {
            won = true;
            mainText.text = "YOU WIN!";
        }
        enemiesLeftText.text = "Enemies Left: " + enemies;
    }
}