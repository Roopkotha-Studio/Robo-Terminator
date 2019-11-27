using UnityEngine;
using UnityEngine.SceneManagement;

public class DataInitializer : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevels = 5;
    [SerializeField] private bool setLevel = true;

    void Awake()
    {
        //Set up level data
        if (setLevel)
        {
            string sceneName = SceneManager.GetActiveScene().name.ToLower();
            if (!PlayerPrefs.HasKey("Level"))
            {
                if (sceneName.Contains("level"))
                {
                    PlayerPrefs.SetInt("IngameLevel", level);
                    if (!PlayerPrefs.HasKey("Restarted")) PlayerPrefs.SetInt("Level", level);
                } else
                {
                    PlayerPrefs.SetInt("Level", 1);
                    PlayerPrefs.SetInt("IngameLevel", 1);
                }
            } else
            {
                if (sceneName.Contains("level"))
                {
                    PlayerPrefs.SetInt("IngameLevel", level);
                    if (!PlayerPrefs.HasKey("Restarted")) PlayerPrefs.SetInt("Level", level);
                }
            }
        }
        if (maxLevels > 0)
        {
            PlayerPrefs.SetInt("MaxLevels", maxLevels);
        } else
        {
            PlayerPrefs.SetInt("MaxLevels", 1);
        }
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}