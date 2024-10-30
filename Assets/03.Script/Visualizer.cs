using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public AudioSource audioSource;                // 오디오 소스
    public GameObject barPrefab;                   // 첫 번째 막대 프리팹
    public GameObject secondBarPrefab;             // 두 번째 막대 프리팹
    public int numberOfBars = 64;                  // 막대 개수
    public float heightMultiplier = 50.0f;         // 막대 높이 배율
    public float spacing = 0.2f;                   // 막대 간 간격
    public float startY = 0.0f;                    // 막대 시작 Y 위치

    private GameObject[] bars;                     // 막대 오브젝트 배열
    private GameObject currentPrefab;              // 현재 사용 중인 프리팹

    void Start()
    {
        currentPrefab = barPrefab;                 // 초기 프리팹 설정
        InitializeBars();
    }

    void Update()
    {
        // 오디오 스펙트럼 데이터 가져오기
        float[] spectrumData = new float[numberOfBars];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 각 막대가 스펙트럼 데이터에 반응하도록 설정
        for (int i = 0; i < numberOfBars; i++)
        {
            int dataIndex = i < numberOfBars / 2 ? i : (numberOfBars - i - 1);
            Vector3 barScale = bars[i].transform.localScale;
            barScale.y = Mathf.Clamp(spectrumData[dataIndex] * heightMultiplier, 0.1f, 10f);
            bars[i].transform.localScale = barScale;

            Vector3 barPosition = bars[i].transform.localPosition;
            barPosition.y = startY + (barScale.y / 2);
            bars[i].transform.localPosition = barPosition;
        }
    }

    // 막대 배열 초기화 및 생성 함수
    void InitializeBars()
    {
        bars = new GameObject[numberOfBars];
        for (int i = 0; i < numberOfBars; i++)
        {
            float xPosition = (i - numberOfBars / 2) * spacing;
            GameObject bar = Instantiate(currentPrefab, new Vector3(xPosition, startY, 0), Quaternion.identity);
            bar.transform.parent = this.transform;
            bars[i] = bar;
        }
    }

    // 현재 사용 중인 프리팹 변경 함수
    public void SetPrefab(bool useSecondPrefab)
    {
        currentPrefab = useSecondPrefab ? secondBarPrefab : barPrefab;

        // 기존 막대들 삭제 후 재생성
        foreach (GameObject bar in bars)
        {
            Destroy(bar);
        }

        InitializeBars();
    }
}
