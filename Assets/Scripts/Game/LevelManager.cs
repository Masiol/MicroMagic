using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            int levelIndex = i + 1;
            if (PlayerPrefs.GetInt("Level_" + levelIndex.ToString(), 0) == 1)
            {
                Debug.Log("Level " + levelIndex + " completed!");
            }
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("Level_" + levelIndex.ToString(), 1);
        Debug.Log("Level " + levelIndex + " completed!");
    }

    public int GetNextLevelIndex()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            int levelIndex = i + 1;
            if (PlayerPrefs.GetInt("Level_" + levelIndex.ToString(), 0) == 0)
            {
                return levelIndex;
            }
        }
        return 1;
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = GetNextLevelIndex();
        if (nextLevelIndex != 0)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
