using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject levelPausePanel, levelCompletePanel, levelFailedPanel;
    public string NextScene;
    public string PreviousScene;

    [Header("Loading Screen")]
    public Image LoadingBar;
    public Text loading_Text;
    public GameObject loadingPanel;
    public float loadingDelayTime = 2f; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(){
PlayButtonClickSound();

    levelPausePanel.SetActive(true);

    }
        public void ResumeGame(){
PlayButtonClickSound();

    levelPausePanel.SetActive(false);

    }
    public void RestartGame(){
PlayButtonClickSound();

            StartCoroutine(LoadSceneWithDelay(NextScene));

    }
    public void LevelCompleted(){

    }
    public void LevelFailed(){

    }
    public void NextLevel(){
PlayButtonClickSound();

    }

    public void HomeGame(){
PlayButtonClickSound();

            StartCoroutine(LoadSceneWithDelay(PreviousScene));

    }
    public void OpenGithubLink()
    {
PlayButtonClickSound();

        Application.OpenURL("https://github.com/qasimmu/2DPlatformerGame.git");
    }
    
        public void OpenItchio()
    {
PlayButtonClickSound();
        Application.OpenURL("https://rainbow-flamingo.itch.io/jumpquest");
    }

        void UpdateLoadingUI(float progress)
    {
        if (LoadingBar) LoadingBar.fillAmount = progress;
        if (loading_Text) loading_Text.text = Mathf.RoundToInt(progress * 100) + "%";
    }
    IEnumerator LoadSceneWithDelay(string name)
    {
if(loadingDelayTime>0){
        loadingPanel.SetActive(true);

}
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
    public void PlayButtonClickSound()
    {
        if (SoundManager._SoundManager)
        {
            SoundManager._SoundManager.playButtonClickSound();

        }
    }
}
