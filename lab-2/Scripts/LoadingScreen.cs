using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public UnityEngine.UI.Slider progressBar;
    public GameObject loadingText;
    public GameObject loadedText;
    private void Start()
    {
        StartCoroutine(LoadLevelAsync(PlayerPrefs.GetInt("loadingScene")));
        PlayerPrefs.DeleteKey("loadingScene");
    }

    private IEnumerator LoadLevelAsync(int level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            progressBar.value = asyncLoad.progress;

            if (asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation)
            {
                loadingText.SetActive(false);
                loadedText.SetActive(true);
                progressBar.value = 1.0f;

                if (Input.anyKeyDown)
                    asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}
