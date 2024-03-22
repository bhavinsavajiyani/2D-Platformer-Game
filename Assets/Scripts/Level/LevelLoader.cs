using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadGivenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(string sceneName)
    {
        LevelStatus status = (LevelStatus) PlayerPrefs.GetInt(sceneName);

        switch (status)
        {
            case LevelStatus.Locked:
                Debug.Log("Level: " +  sceneName + " is locked...");
                break;

            case LevelStatus.Unlocked:
                SceneManager.LoadScene(sceneName);
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(sceneName);
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
