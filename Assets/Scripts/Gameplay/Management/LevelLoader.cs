using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour 
{
    public static LevelLoader instance;

    int numLevels;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        numLevels = SceneManager.sceneCountInBuildSettings;
    }

    public int curLevel = 0;

    public void LoadLevel(int level)
    {
        if (level < 0 || level > numLevels - 1)
        {
            Debug.LogError("Invalid level index");
            return;
        }

        curLevel++;
        SceneManager.LoadScene(curLevel);
    }

    public void LoadNextLevel()
    {
        if (curLevel < numLevels)
        {
            curLevel++;
        }
        else
        {
            curLevel = 0;
        }
        SceneManager.LoadScene(curLevel);
    }
}
