using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject StagePanel; // �������� ���� �г�
    public GameObject Fadein; // ���̵��� �г�
    public GameObject Fadeout; // ���̵�ƿ� �г�
    public GameObject CharPicPanel; // ĳ���� ���ϴ� â �г�
    public GameObject SettingPanel; // �ΰ��� ����â �г�
    public GameObject TitleSettingPanel; // Ÿ��Ʋ ����â �г�
    public GameObject VolumPanel; // �������� ���� �г�
    public GameObject CreditPanel; // ũ���� �г�
    public GameObject MethodPanel; // ����â �г�
    public GameObject ExitPanel; // �����г�
    public GameObject LanguagePanel; // ����г�
    public GameObject KeySetPanel; // Ű�����г�
    public GameObject CountDownObject; // ī��Ʈ �ٿ�


    [Header("Other Components")]
    public Animator anim;
    public Music music;

    private Stack<GameObject> panelStack = new Stack<GameObject>(); // �г� ����

    public bool isSetting = false; // ���� ����â����
    public bool isCharPanel = false; // ���� ĳ���� â����
    public bool isTitleSettingPanel = false; // ���� Ÿ��Ʋ ���� â����
    public bool isCountDown = false; // ���� ī��Ʈ �ٿ� ������
    public bool isNavimpossible = false; // �׺���̼��� ��������


    private void Start()
    {
        Fadeout.SetActive(true);
        StartCoroutine(CountDown()); // ���� ���۽� ESC �� ������
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
                if (music.audioSource.isPlaying) // ������ ��� ���̶�� �ߴ��մϴ�.
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

    public void ButtonPanelOff() // ��ư �гε� ����
    {
        VolumPanel.SetActive(false);
        CreditPanel.SetActive(false);
        MethodPanel.SetActive(false);
        TitleSettingPanel.SetActive(false);
        LanguagePanel.SetActive(false);
        KeySetPanel.SetActive(false);
    }

    public void PanelSetActive(GameObject Panel) // �г� �����ְ� ���ؿ� �߰� ���� �г��� �� �� �޼���θ� ���� ���ݱ��� �ϳ��ϳ� �߰��ϸ鼭 �ϵ��ڵ��ߴµ� ���� �̰� �ϳ��ΰ��� ���̽�
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
        if (music.audioSource.isPlaying) // ������ ��� ���̶�� �ߴ��մϴ�.
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

    public void Retry(string Stage) // �ٽ� ����
    {
        SceneManager.LoadScene(Stage);
        Time.timeScale = 1;
    }

    public void Title(string SceneName) // Ÿ��Ʋ �� ����
    {
        LoadingManager.LoadScene(SceneName);
        Time.timeScale = 1;
    }

    public void OpenStage() // �������� â ����
    {
        StagePanel.SetActive(true);
        panelStack.Push(StagePanel);
    }

    public void CloseStage() // �������� â �ݱ�
    {
        StagePanel.SetActive(false);
    }

    public void firstStage()
    {
        Fadein.SetActive(true);
        StartCoroutine(SceneLate(0.7f, "Stage1"));
    }

    public void OffPic() // ĳ���� ��â �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        isCharPanel = false;
        CharPicPanel.SetActive(false);
    }

    public void OnPic() // ĳ���� ��â ����
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1f, 1f), 1);
        ButtonPanelOff();
        OffTitleSetting();
        CharPicPanel.SetActive(true);
        isCharPanel = true;
        panelStack.Push(CharPicPanel);
        // StagerManager.instance.currentStage = StagerManager.Stage.CharPanel;
    }

    public void OnTitleSetting() // ����â ����
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        isTitleSettingPanel = true;
        TitleSettingPanel.SetActive(true);
        panelStack.Push(TitleSettingPanel);
    }

    public void OffTitleSetting() // ����â �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        isTitleSettingPanel = false;
        TitleSettingPanel.SetActive(false);
    }

    public void OnVolumPanel() // ����â ����
    {
        VolumPanel.SetActive(true);
        panelStack.Push(VolumPanel);
    }

    public void OffVolumPanel() // ����â �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        VolumPanel.SetActive(false);
    }

    public void OnCreditPanel() // ũ���� â ����
    {
        CreditPanel.SetActive(true);
        panelStack.Push(CreditPanel);
    }

    public void OffCreditPanel() // ũ���� â �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        CreditPanel.SetActive(false);
    }

    public void OnMethodPanel() // ����â ����
    {
        MethodPanel.SetActive(true);
        panelStack.Push(MethodPanel);
    }

    public void OffMethodPanel() // ����â �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        MethodPanel.SetActive(false);
    }

    public void OnExitPanel() // �����г� ����
    {
        ExitPanel.SetActive(true);
        panelStack.Push(ExitPanel);
    }

    public void OffExitPanel() // �����г� �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        ExitPanel.SetActive(false);
    }

    public void OnLanguagePanel() // ����г� ����
    {
        LanguagePanel.SetActive(true);
        panelStack.Push(LanguagePanel);
    }

    public void OffLanguagePanel() // ����г� �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        LanguagePanel.SetActive(false);
    }

    public void OnKeySetPanel() // Ű�����г� ����
    {
        KeySetPanel.SetActive(true);
        panelStack.Push(KeySetPanel);
    }

    public void OffKeySetPanel() // Ű�����г� �ݱ�
    {
        AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.0f, 1.0f), 1);
        KeySetPanel.SetActive(false);
    }

    public void GameExit() // ���� �޴�
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


    public IEnumerator SceneLate(float time, string sceneName) // �� �ʰ� �̵�
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
        if (!music.audioSource.isPlaying) // ������ �ߴܵǾ��ٸ� �ٽ� ����մϴ�.
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
    public void StackPush(GameObject gameObject) // ���ؿ� �г� �߰�
    {
        panelStack.Push(gameObject);

    }
    public void IngameLodaingSceneLoad(string SceneName) // �ΰ��ӿ��� �������� ���� â �ε�
    {
        Time.timeScale = 1;
        LoadingManager.LoadScene(SceneName);
    }
}
