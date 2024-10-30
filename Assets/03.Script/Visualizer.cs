using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public AudioSource audioSource;                // ����� �ҽ�
    public GameObject barPrefab;                   // ù ��° ���� ������
    public GameObject secondBarPrefab;             // �� ��° ���� ������
    public int numberOfBars = 64;                  // ���� ����
    public float heightMultiplier = 50.0f;         // ���� ���� ����
    public float spacing = 0.2f;                   // ���� �� ����
    public float startY = 0.0f;                    // ���� ���� Y ��ġ

    private GameObject[] bars;                     // ���� ������Ʈ �迭
    private GameObject currentPrefab;              // ���� ��� ���� ������

    void Start()
    {
        currentPrefab = barPrefab;                 // �ʱ� ������ ����
        InitializeBars();
    }

    void Update()
    {
        // ����� ����Ʈ�� ������ ��������
        float[] spectrumData = new float[numberOfBars];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // �� ���밡 ����Ʈ�� �����Ϳ� �����ϵ��� ����
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

    // ���� �迭 �ʱ�ȭ �� ���� �Լ�
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

    // ���� ��� ���� ������ ���� �Լ�
    public void SetPrefab(bool useSecondPrefab)
    {
        currentPrefab = useSecondPrefab ? secondBarPrefab : barPrefab;

        // ���� ����� ���� �� �����
        foreach (GameObject bar in bars)
        {
            Destroy(bar);
        }

        InitializeBars();
    }
}
