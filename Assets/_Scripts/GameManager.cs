using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject levelPausePanel, levelCompletePanel, levelFailedPanel;
    public string NextScene;
    public string PreviousScene;

    [Header("Loading Screen")]
    public Image LoadingBar;
    public Text loading_Text;
    public GameObject loadingPanel;
    public float loadingDelayTime = 2f; 
    public GameObject[] Levels;

public GameObject PlayerObject;
public GameObject PlayerStartingPosition;
    private void Awake() {
                if(instance==null){
            instance=this;
        }
    }

    public void CheckLevelFailorSuccessfull(bool isFail){
        if(isFail){
            levelFailedPanel.SetActive(true);
            Time.timeScale=0;

        }else{
            levelCompletePanel.SetActive(true);
            Time.timeScale=0;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
            Time.timeScale=1f;
            Levels[PlayerPrefs.GetInt("SelectedLevel")].gameObject.SetActive(true);
            PlayerObject.transform.position=PlayerStartingPosition.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(){
PlayButtonClickSound();
            Time.timeScale=0;

    levelPausePanel.SetActive(true);

    }
        public void ResumeGame(){
PlayButtonClickSound();
            Time.timeScale=1;

    levelPausePanel.SetActive(false);

    }
    public void RestartGame(){
PlayButtonClickSound();
            Time.timeScale=1;

            StartCoroutine(LoadSceneWithDelay(NextScene));

    }

    public void NextLevel(){
PlayButtonClickSound();
        if (SoundManager._SoundManager)
        {
            SoundManager._SoundManager.playButtonClickSound();
        }
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("SelectedLevel") < Levels.Length - 1)
        {
            PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel") + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            HomeGame();
        }
    }

    public void HomeGame(){
PlayButtonClickSound();
Time.timeScale=1;
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
