using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentSceneIndex;
    private int levelCount;
    private int loadingScreenScene;
    private bool currentlyLoading;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelCount = SceneManager.sceneCountInBuildSettings - 1;
        loadingScreenScene = levelCount;    // Экран загрузки - последняя сцена в списке
    }

    public int GetCurrentLevelIndex()
    {
        return currentSceneIndex;
    }

    public void ReloadLevel()
    {
        LoadLevel(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        if (levelCount - 1 != currentSceneIndex)
        {
            if (currentSceneIndex + 1 > PlayerPrefs.GetInt("currentLevel"))
                PlayerPrefs.SetInt("currentLevel", currentSceneIndex + 1);

            LoadLevel(currentSceneIndex + 1);
        }
        else
            LoadLevel(0);
    }

    public void LoadLevel(int level)
    {
        if (currentlyLoading) return;

        currentlyLoading = true;
        PlayerPrefs.SetInt("loadingScene", level);
        Time.timeScale = 1f;
        SceneManager.LoadScene(loadingScreenScene);
    }

}
