using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class StagerManager : MonoBehaviour
{
    public enum Stage
    {
        FirstStage,
        SecondStage,
        ThirdStage,
        fourthStage,
        fifthStage,
        CharPanel,
        TitleSettingPanel
    }

    public string[] songPath;// 각 스테이지별 음악 파일 경로

    public static StagerManager instance; // 싱글톤 인스턴스
    public GameObject Fadein; // 페이드 인 효과 오브젝트
    public ButtonManager buttonManager;// 버튼 매니저 참조

    bool isStart;// 게임 시작 여부

    [SerializeField] GameObject fixedPanel; // 고정된 패널

    bool charpanel = false;


    public Stage currentStage;// 현재 스테이지

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;// 씬 로드 시 이벤트 연결
    }
    
    void Update()
    {
        // 이거를 지우고 하나의 로딩씬으로 가게 바꾸고 이걸 다른거에 옮겨 코루틴이랑 같이
        if (Input.GetKeyDown(KeyCode.Return) && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        if (!isStart)// 게임이 시작되지 않았을 때만 실행
        {
            DataManager.instance.songPath = songPath[(int)currentStage];// 현재 스테이지의 음악 경로 설정
            if (currentStage == Stage.FirstStage) // 각 스테이지에 따른 효과음과 카메라 흔들림 효과
            { 
            AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);
            CameraShake.instance.Shake();

            Fadein.SetActive(true);
            StartCoroutine(SceneLate(1));
        }
        else if (currentStage == Stage.SecondStage)
        {
            AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);
            CameraShake.instance.Shake();

            Fadein.SetActive(true);
            StartCoroutine(SceneLate(2));
        }   
        else if (currentStage == Stage.ThirdStage)
        {
            AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);
            CameraShake.instance.Shake();

            Fadein.SetActive(true);
            StartCoroutine(SceneLate(3));
        }
          else if (currentStage == Stage.fifthStage)
        {
            AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1.0f, 1.0f), 1);
            CameraShake.instance.Shake();

            Fadein.SetActive(true);
            StartCoroutine(SceneLate(4));
        }
        else
        {
            AudioManager.instance.PlaySound(transform.position, 5, Random.Range(1.0f, 1.0f), 1);
            FixedPanel();
        }
        }

    }
    void FixedPanel()   // 고정된 패널을 숨기고 다시 보이게 함
    {
        fixedPanel.SetActive(false);
        fixedPanel.SetActive(true);
    }
    IEnumerator SceneLate(int num)
    {
        isStart = true;// 게임 시작 플래그 설정
        yield return new WaitForSeconds(0.7f); // 0.7초 대기 후 씬 로드
        SceneManager.LoadScene(num); // 해당 번호의 씬 로드
    }

    public void CharPanel()
    {
        charpanel = true;// 캐릭터 패널 열기
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 로드 완료 시 추가 작업이 필요하다면 여기에 구현
    }
}