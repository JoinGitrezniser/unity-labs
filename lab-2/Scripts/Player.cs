using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public LevelManager levelManager;
    public float defeatHeight;
    public int collectableCount;
    public float speed;
    private int collectedCount;
    private Rigidbody rb;
    private float time;
    private float victoryCountdownTime;
    private bool isVictoryTimerCounting;
    public TMP_Text Score;
    public TMP_Text Victory;
    public TMP_Text TimeCounter;
    public GameOverScreen gameOverScreen;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collectedCount = 0;

        Victory.text = "";
        time = 0;
        UpdateScore();
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (isVictoryTimerCounting)
        {
            victoryCountdownTime -= Time.deltaTime;

            if (victoryCountdownTime <= 0)
            {
                int currentSceneIndex = levelManager.GetCurrentLevelIndex();
                int completionTime = Mathf.FloorToInt(time);
                string completionTimeKey = $"completionTime-{currentSceneIndex}";

                gameOverScreen.ShowVictory(completionTime);

                if (PlayerPrefs.GetInt(completionTimeKey, 40) > completionTime)
                    PlayerPrefs.SetInt(completionTimeKey, completionTime);

                if (currentSceneIndex + 1 > PlayerPrefs.GetInt("currentLevel"))
                    PlayerPrefs.SetInt("currentLevel", currentSceneIndex + 1);
            }
            Victory.text = $"Victory! {Mathf.FloorToInt(victoryCountdownTime + 1)}...";
        }
        else
        {
            time += Time.deltaTime;
            TimeCounter.text = $"Time: {Mathf.FloorToInt(time)} s";
        }
    }


    private void UpdateScore()
    {
        Score.text = $"Score: {collectedCount}";

        if (collectedCount >= collectableCount)
        {
            victoryCountdownTime = 5;
            isVictoryTimerCounting = true;
            UpdateTime();
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y <= defeatHeight)
            gameOverScreen.ShowDefeat();


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        UpdateTime();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            collectedCount += 1;
            speed += 1;

            UpdateScore();
        }
    }
}
