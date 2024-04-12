using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class LevelStars : MonoBehaviour
{
    public GameObject star_1;
    public GameObject star_2;
    public GameObject star_3;
    public int levelIndex;
    private int completionTime;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStarStatus(PlayerPrefs.GetInt($"completionTime-{levelIndex}", 40));
    }

    public void UpdateStarStatus(int completionTime)
    {

        if (completionTime >= 20)
            star_3.SetActive(false);
        if (completionTime >= 30)
            star_2.SetActive(false);
        if (completionTime >= 40)
            star_1.SetActive(false);
    }
}
