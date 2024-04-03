using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    void Awake()
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

    void Start()
    {
        // Przechodzimy przez wszystkie sceny i sprawdzamy, czy jakiekolwiek z nich zosta�y uko�czone
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            int levelIndex = i + 1; // Indeksowanie sceny od 1 (pomijaj�c scen� startow�)
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
        return 1; // Je�li wszystkie poziomy zosta�y uko�czone, zwracamy pierwszy poziom
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
            // Tutaj mo�esz doda� logik�, je�li chcesz, �eby gra zako�czy�a si� po uko�czeniu wszystkich poziom�w
        }
    }
}
