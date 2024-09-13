using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class StageModeStageManager : MonoBehaviour
{
    public enum Stage
    {
        Main,
        FirstTheFirstStage,
        FirstTheSecondStage,
        FirstTheThirdStage,
        FirstThefourthStage,
        FirstThefifthStage,
        FirstTheSixthStage,
        FirstTheSeventhStage,
        FirstTheEighthStage,

        SecondTheFirstStage,
        SecondTheSecondStage,
        SecondTheThirdStage,
        SecondThefourthStage,
        SecondThefifthStage,
        SecondTheSixthStage,
        SecondTheSeventhStage,
        SecondTheEighthStage,

        ThirdTheFirstStage,
        ThirdTheSecondStage,
        ThirdTheThirdStage,
        ThirdThefourthStage,
        ThirdThefifthStage,
        ThirdTheSixthStage,
        ThirdTheSeventhStage,
        ThirdTheEighthStage,

    }

    public string[] songPath;

    public static StageModeStageManager instance;
    public GameObject Fadein;
    public ButtonManager buttonManager;

    bool isStart;

    [SerializeField] GameObject fixedPanel;

    [SerializeField] GameObject GravityPanel;


    bool charpanel = false;


    public Stage currentStage;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // 이거를 지우고 하나의 로딩씬으로 가게 바꾸고 이걸 다른거에 옮겨 코루틴이랑 같이
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
            Debug.Log("D");
        }
    }
    public void StartGame()
    {
        if (!isStart)
        {
            DataManager.instance.songPath = songPath[(int)currentStage];
            if (currentStage == Stage.FirstTheFirstStage)
            {
                AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);

                StartCoroutine(SceneLate("StagdeModeStage1"));
                Fadein.SetActive(true);

            }
            else if (currentStage == Stage.FirstTheSecondStage)
            {
                AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);

                Fadein.SetActive(true);
                StartCoroutine(SceneLate("Stage2"));
            }
            else if (currentStage == Stage.FirstTheThirdStage)
            {
            }
            else if (currentStage == Stage.FirstThefourthStage)
            {
            }
            else if (currentStage == Stage.FirstThefifthStage)
            {
            }
            else if (currentStage == Stage.FirstTheSixthStage)
            {
            }
            else if (currentStage == Stage.FirstTheSeventhStage)
            {
            }
            else if (currentStage == Stage.FirstTheEighthStage)
            {
                GravityPanel.SetActive(true);  
                Fadein.SetActive(true);
                StartCoroutine(SceneLate("Stage5"));
            }
            else if (currentStage == Stage.SecondTheFirstStage)
            {
                AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);

                Fadein.SetActive(true);
                StartCoroutine(SceneLate("StagdeModeStage2"));
            }
            else if (currentStage == Stage.SecondTheSecondStage)
            {
            }
            else if (currentStage == Stage.SecondTheThirdStage)
            {
            }
            else if (currentStage == Stage.SecondThefourthStage)
            {
            }
            else if (currentStage == Stage.SecondThefifthStage)
            {
            }
            else if (currentStage == Stage.SecondTheSixthStage)
            {
            }
            else if (currentStage == Stage.SecondTheSeventhStage)
            {
            }
            else if (currentStage == Stage.SecondTheEighthStage)
            {
            }
            else if (currentStage == Stage.ThirdTheFirstStage)
            {
                AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);

                Fadein.SetActive(true);
                StartCoroutine(SceneLate("StagdeModeStage3"));
            }
            else if (currentStage == Stage.ThirdTheSecondStage)
            {
            }
            else if (currentStage == Stage.ThirdTheThirdStage)
            {
            }
            else if (currentStage == Stage.ThirdThefourthStage)
            {
            }
            else if (currentStage == Stage.ThirdThefifthStage)
            {
            }
            else if (currentStage == Stage.ThirdTheSixthStage)
            {
            }
            else if (currentStage == Stage.ThirdTheSeventhStage)
            {
            }
            else if (currentStage == Stage.ThirdTheEighthStage)
            {
            }
            else
            {
                GravityPanel.SetActive(false);
                //AudioManager.instance.PlaySound(transform.position, 5, Random.Range(1.0f, 1.0f), 1);
                // FixedPanel();
            }
        }

    }
    void FixedPanel()
    {
        fixedPanel.SetActive(false);
        fixedPanel.SetActive(true);
    }
    IEnumerator SceneLate(string sceneName)
    {
        isStart = true;
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneName);
    }

    public void CharPanel()
    {
        charpanel = true;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }
}