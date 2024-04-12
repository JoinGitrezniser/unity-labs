using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject nextButton;
    public TMP_Text gameOverText;
    public GameObject gameOverScreenPanel;
    public GameObject starContainer;
    public LevelStars levelStars;

    public void ShowVictory(int completionTime)
    {
        if (gameOverScreenPanel.activeSelf) return;

        Time.timeScale = 0.0f;
        gameOverScreenPanel.SetActive(true);
        restartButton.SetActive(false);
        nextButton.SetActive(true);
        starContainer.SetActive(true);
        levelStars.UpdateStarStatus(completionTime);
        
        gameOverText.text = "You win!";
    }
    public void ShowDefeat()
    {
        if (gameOverScreenPanel.activeSelf) return;

        Time.timeScale = 0.0f;
        gameOverScreenPanel.SetActive(true);
        restartButton.SetActive(true);
        nextButton.SetActive(false);
        gameOverText.text = "You lost! :(";
    }
}
