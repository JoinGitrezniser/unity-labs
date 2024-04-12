using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IngameMenu : MonoBehaviour
{
    public LevelManager levelManager;

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        levelManager.LoadLevel(0);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
