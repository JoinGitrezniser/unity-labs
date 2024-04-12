using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentSceneIndex;
    private int sceneCount;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        if (sceneCount - 1 != currentSceneIndex)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
            ReloadLevel();
    }
}
