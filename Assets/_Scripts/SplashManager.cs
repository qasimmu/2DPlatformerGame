using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public string NextScene;
    public string PreviousScene;

    [Header("Loading Screen")]
    public Image LoadingBar;
    public Text loading_Text;
    public GameObject loadingPanel;
    public float loadingDelayTime = 2f; 

    void Start()
    {
        StartCoroutine(LoadSceneWithDelay(NextScene));
    }

    IEnumerator LoadSceneWithDelay(string name)
    {

        loadingPanel.SetActive(true);
        float timer = 0f;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;
        while (timer < loadingDelayTime)
        {
            timer += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(timer / loadingDelayTime);
            UpdateLoadingUI(fillAmount);
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        asyncOperation.allowSceneActivation = true;

    }

    void UpdateLoadingUI(float progress)
    {
        if (LoadingBar) LoadingBar.fillAmount = progress;
        if (loading_Text) loading_Text.text = Mathf.RoundToInt(progress * 100) + "%";
    }
}
