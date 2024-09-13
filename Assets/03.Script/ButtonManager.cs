using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject StagePanel; // 스테이지 고르는 패널
    public GameObject Fadein; // 페이드인 패널
    public GameObject Fadeout; // 페이드아웃 패널
    public GameObject CharPicPanel; // 캐릭터 픽하는 창 패널
    public GameObject SettingPanel; // 인게임 설정창 패널
    public GameObject TitleSettingPanel; // 타이틀 설정창 패널
    public GameObject VolumPanel; // 볼륨조절 세팅 패널
    public GameObject CreditPanel; // 크레딧 패널
    public GameObject MethodPanel; // 설명창 패널
    public GameObject ExitPanel; // 종료패널
    public GameObject LanguagePanel; // 언어패널
    public GameObject KeySetPanel; // 키세팅패널
    public GameObject CountDownObject; // 카운트 다운


    [Header("Other Components")]
    public Animator anim;
    public Music music;

    private Stack<GameObject> panelStack = new Stack<GameObject>(); // 패널 스택

    public bool isSetting = false; // 현재 세팅창인지
    public bool isCharPanel = false; // 현재 캐릭터 창인지
    public bool isTitleSettingPanel = false; // 현재 타이틀 세팅 창인지
    public bool isCountDown = false; // 현재 카운트 다운 중인지
    public bool isNavimpossible = false; // 네비게이션이 가능한지


    private void Start()
    {
        Fadeout.SetActive(true);
        StartCoroutine(CountDown()); // 게임 시작시 ESC 못 누르게
    }

    void Update()
    {
        if (isNavimpossible && Input.GetKeyDown(KeyCode.Escape)) isNavimpossible = false;

        if (isTitleSettingPanel && Input.GetKeyDown(KeyCode.Escape)) isTitleSettingPanel = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelStack.Count > 0)
            {
                GameObject topPanel = panelStack.Pop();
                topPanel.SetActive(false);
                UpdatePanelFlags(topPanel, false);
                Play();
            }
            else if (!isSetting && !isCharPanel && SettingPanel != null && !isCountDown)
            {
                SettingPanel.SetActive(true);
                if (music.audioSource.isPlaying) // 음악이 재생 중이라면 중단합니다.
                    music.audioSource.Pause();
                Time.timeScale = 0;
                isSetting = true;
                panelStack.Push(SettingPanel);
            }
            else
            {
               ExitPanel.SetActive(true);
                panelStack.Push(ExitPanel);
            }
        }
    }

    public void UpdatePanelFlags(GameObject panel, bool state)
    {
        if (panel == CharPicPanel) isCharPanel = state;
        else if (panel == TitleSettingPanel) isTitleSettingPanel = state;
        else if (panel == SettingPanel) isSetting = state;
    }

    public void ButtonPanelOff() // 버튼 패널들 끄기
    {
        VolumPanel.SetActive(false);
        CreditPanel.SetActive(false);
        MethodPanel.SetActive(false);
        TitleSettingPanel.SetActive(false);
        LanguagePanel.SetActive(false);
        KeySetPanel.SetActive(false);
    }

    public void PanelSetActive(GameObject Panel) // 패널 열어주고 스텍에 추가 이제 패널은 이 한 메서드로만 가능 지금까지 하나하나 추가하면서 하드코딤했는데 이제 이거 하나로가능 나이스
    {
        isTitleSettingPanel = true;
        Panel.SetActive(true);
        panelStack.Push(Panel);
    }
    public void PanelSetActiveSound(int soungIndex)
    {
        AudioManager.instance.PlaySound(transform.position, soungIndex, Random.Range(1.0f, 1.0f), 1);
    }

    public void Stop()
    {
        if (isCountDown)
            return;
        if (music.audioSource.isPlaying) // 음악이 재생 중이라면 중단합니다.
            music.audioSource.Pause();
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
        panelStack.Push(SettingPanel);
    }

    public void Play()
    {
        if (isCountDown || SettingPanel == null)
            return;
        isSetting = false;
        SettingPanel.SetActive(false);
        CountDownObject.SetActive(false);
        CountDownObject.SetActive(true);
        StartCoroutine(PlayCount());
    }

    public void Retry(string Stage) // 다시 시작
    {
        SceneManager.LoadScene(Stage);
        Time.timeScale = 1;
    }

    public void Title(string SceneName) // 타이틀 씬 가기
    {
        LoadingManager.LoadScene(SceneName);
        Time.timeScale = 1;
    }

    public void OpenStage() // 스테이지 창 열기
    {
        StagePanel.SetActive(true);
        panelStack.Push(StagePanel);
    }

    public void CloseStage() // 스테이지 창 닫기
    {
        StagePanel.SetActive(false);
    }

    public void firstStage()
    {
        Fadein.SetActive(true);
        StartCoroutine(SceneLate(0.7f, "Stage1"));
    }

    public void OffPic() // 캐릭터 픽창 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        isCharPanel = false;
        CharPicPanel.SetActive(false);
    }

    public void OnPic() // 캐릭터 픽창 열기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1f, 1f), 1);
        ButtonPanelOff();
        OffTitleSetting();
        CharPicPanel.SetActive(true);
        isCharPanel = true;
        panelStack.Push(CharPicPanel);
        // StagerManager.instance.currentStage = StagerManager.Stage.CharPanel;
    }

    public void OnTitleSetting() // 설정창 열기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        isTitleSettingPanel = true;
        TitleSettingPanel.SetActive(true);
        panelStack.Push(TitleSettingPanel);
    }

    public void OffTitleSetting() // 설정창 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        isTitleSettingPanel = false;
        TitleSettingPanel.SetActive(false);
    }

    public void OnVolumPanel() // 볼륨창 열기
    {
        VolumPanel.SetActive(true);
        panelStack.Push(VolumPanel);
    }

    public void OffVolumPanel() // 볼륨창 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        VolumPanel.SetActive(false);
    }

    public void OnCreditPanel() // 크레딧 창 열기
    {
        CreditPanel.SetActive(true);
        panelStack.Push(CreditPanel);
    }

    public void OffCreditPanel() // 크레딧 창 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        CreditPanel.SetActive(false);
    }

    public void OnMethodPanel() // 설명창 열기
    {
        MethodPanel.SetActive(true);
        panelStack.Push(MethodPanel);
    }

    public void OffMethodPanel() // 설명창 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        MethodPanel.SetActive(false);
    }

    public void OnExitPanel() // 종료패널 열기
    {
        ExitPanel.SetActive(true);
        panelStack.Push(ExitPanel);
    }

    public void OffExitPanel() // 종료패널 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        ExitPanel.SetActive(false);
    }

    public void OnLanguagePanel() // 언어패널 열기
    {
        LanguagePanel.SetActive(true);
        panelStack.Push(LanguagePanel);
    }

    public void OffLanguagePanel() // 언어패널 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        LanguagePanel.SetActive(false);
    }

    public void OnKeySetPanel() // 키세팅패널 열기
    {
        KeySetPanel.SetActive(true);
        panelStack.Push(KeySetPanel);
    }

    public void OffKeySetPanel() // 키세팅패널 닫기
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        KeySetPanel.SetActive(false);
    }

    public void GameExit() // 메인 메뉴
    {
        Fadein.SetActive(true);
        StartCoroutine(SceneLate(1.3f, "Menu")); ;
    }

    public void SecondStage()
    {
        Fadein.SetActive(true);
        StartCoroutine(SceneLate2());
    }
    public void StageScenceLoad(string sceneName)
    {
        AudioManager.instance.PlaySound(transform.position, 11, Random.Range(1.0f, 1.0f), 1);
        Fadein.SetActive(true);
        StartCoroutine(SceneLate(1.3f, sceneName));
    }


    public IEnumerator SceneLate(float time, string sceneName) // 씬 늦게 이동
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator SceneLate2()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(2);
    }

    IEnumerator PlayCount()
    {
        isCountDown = true;
        yield return new WaitForSecondsRealtime(1.7f);
        Time.timeScale = 1;
        if (!music.audioSource.isPlaying) // 음악이 중단되었다면 다시 재생합니다.
            music.audioSource.Play();
        isCountDown = false;
    }

    IEnumerator CountDown()
    {
        isCountDown = true;
        yield return new WaitForSecondsRealtime(2f);
        isCountDown = false;
    }
    public void IsNikEditstrue()
    {
        isNavimpossible = true;
    }
    public void IsNikEditsflase()
    {
        isNavimpossible = false;
    }
    public void StackPush(GameObject gameObject) // 스텍에 패널 추가
    {
        panelStack.Push(gameObject);

    }
    public void IngameLodaingSceneLoad(string SceneName) // 인게임에서 스테이지 선택 창 로드
    {
        Time.timeScale = 1;
        LoadingManager.LoadScene(SceneName);
    }
}
