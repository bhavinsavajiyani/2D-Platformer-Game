using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelStatus
{
    Locked,
    Unlocked,
    Completed
}

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public string[] Levels;

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
        if(PlayerPrefs.GetInt("Level1") == (int) LevelStatus.Locked)
        {
            SetLevelStatus("Level1", LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus status = (LevelStatus) PlayerPrefs.GetInt(level, 0);
        return status;
    }

    public void SetLevelStatus(string level, LevelStatus status)
    {
        PlayerPrefs.SetInt(level, (int)status);
    }

    public void LevelComplete()
    {
        // Mark current level status as completed
        Scene currentScene = SceneManager.GetActiveScene();
        SetLevelStatus(currentScene.name, LevelStatus.Completed);

        // Unlock Next Level
        int currentSceneIndex = Array.FindIndex(Levels, level => level == currentScene.name);
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < Levels.Length)
        {
            SetLevelStatus(Levels[nextSceneIndex], LevelStatus.Unlocked);
            SceneManager.LoadScene(nextSceneIndex);
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
