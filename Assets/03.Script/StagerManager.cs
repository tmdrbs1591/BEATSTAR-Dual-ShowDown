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

    public string[] songPath;// �� ���������� ���� ���� ���

    public static StagerManager instance; // �̱��� �ν��Ͻ�
    public GameObject Fadein; // ���̵� �� ȿ�� ������Ʈ
    public ButtonManager buttonManager;// ��ư �Ŵ��� ����

    bool isStart;// ���� ���� ����

    [SerializeField] GameObject fixedPanel; // ������ �г�

    bool charpanel = false;


    public Stage currentStage;// ���� ��������

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

        SceneManager.sceneLoaded += OnSceneLoaded;// �� �ε� �� �̺�Ʈ ����
    }
    
    void Update()
    {
        // �̰Ÿ� ����� �ϳ��� �ε������� ���� �ٲٰ� �̰� �ٸ��ſ� �Ű� �ڷ�ƾ�̶� ����
        if (Input.GetKeyDown(KeyCode.Return) && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        if (!isStart)// ������ ���۵��� �ʾ��� ���� ����
        {
            DataManager.instance.songPath = songPath[(int)currentStage];// ���� ���������� ���� ��� ����
            if (currentStage == Stage.FirstStage) // �� ���������� ���� ȿ������ ī�޶� ��鸲 ȿ��
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
    void FixedPanel()   // ������ �г��� ����� �ٽ� ���̰� ��
    {
        fixedPanel.SetActive(false);
        fixedPanel.SetActive(true);
    }
    IEnumerator SceneLate(int num)
    {
        isStart = true;// ���� ���� �÷��� ����
        yield return new WaitForSeconds(0.7f); // 0.7�� ��� �� �� �ε�
        SceneManager.LoadScene(num); // �ش� ��ȣ�� �� �ε�
    }

    public void CharPanel()
    {
        charpanel = true;// ĳ���� �г� ����
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �ε� �Ϸ� �� �߰� �۾��� �ʿ��ϴٸ� ���⿡ ����
    }
}