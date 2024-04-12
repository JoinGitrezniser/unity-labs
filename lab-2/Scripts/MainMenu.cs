using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public LevelManager levelManager;

    public void LoadLevel(int level)
    {
        levelManager.LoadLevel(level);
    }

    public void MenuExit()
    {
        Application.Quit();
    }
}
