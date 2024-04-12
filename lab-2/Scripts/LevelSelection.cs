using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Image[] buttons;

    private void Start()
    {
        ResetLevelButtons();
    }

    private void ResetLevelButtons()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 > currentLevel)
                buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            PlayerPrefs.DeleteAll();

        ResetLevelButtons();
    }
}
