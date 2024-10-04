using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("Levels Parent")]
    public GameObject levelsParent;
    public GameObject[] Levels;
    [SerializeField]int totalLevelsUnlocked;

    public string NextScene;
    public string PreviousScene;

    [Header("Loading Screen")]
    public Image LoadingBar;
    public Text loading_Text;
    public GameObject loadingPanel;
    public float loadingDelayTime = 2f; 

    void Start()
    {
        Levels = new GameObject[levelsParent.transform.childCount];
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i] = levelsParent.transform.GetChild(i).gameObject;
            Levels[i].transform.gameObject.GetComponent<Button>().interactable=false;

        }
        
        initLevelSelection();
    }



    void initLevelSelection()
    {
        totalLevelsUnlocked = PlayerPrefs.GetInt("TotalLevelsUnlocked");
        for (int i = 0; i <= totalLevelsUnlocked; i++)
        {
            Levels[i].transform.gameObject.GetComponent<Button>().interactable=true;
        }
    }

    public void selectLevel(int temp)
    {
        if (!Levels[temp].transform.GetChild(0).gameObject.activeSelf)
        {

            PlayerPrefs.SetInt("SelectedLevel", temp);

            StartCoroutine(LoadSceneWithDelay(NextScene));

        }
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
    
    public void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("TotalLevelsUnlocked", Levels.Length -1);
        initLevelSelection();
    }

    public void BackBtn(){
            StartCoroutine(LoadSceneWithDelay(PreviousScene));

    }
 public void OpenGithubLink()
    {

        Application.OpenURL("https://github.com/qasimmu/2DPlatformerGame.git");
    }
    
        public void OpenItchio()
    {

        Application.OpenURL("https://rainbow-flamingo.itch.io/jumpquest");
    }
    
}
