using UnityEngine;
using UnityEngine.UI;

public class VolumeBasedImageScaler : MonoBehaviour
{
    public AudioSource audioSource;  // ����� �ҽ�
    public Image targetImage;        // ũ�⸦ ������ �̹���
    public float scaleMultiplier = 2.0f; // ������ ���� �� (�̹��� ũ�� ����)
    public float smoothSpeed = 0.1f; // �̹��� ũ�� ��ȭ �ӵ� ����

    private Vector3 originalScale;   // ���� �̹��� ũ��

    void Start()
    {
        // �̹����� ���� ũ�� ����
        originalScale = targetImage.transform.localScale;
    }

    void Update()
    {
        // ���� ���� ���� �� (RMS ��)�� ���
        float volume = GetAudioVolume();

        // �̹��� ũ�⸦ ������ ������ ���� ���� (�ε巴�� ��ȭ)
        Vector3 newScale = originalScale * (1 + volume * scaleMultiplier);
        targetImage.transform.localScale = Vector3.Lerp(targetImage.transform.localScale, newScale, smoothSpeed);
    }

    // ���� ������ ���� (��� ����)�� ����ϴ� �Լ�
    float GetAudioVolume()
    {
        // ����� �ҽ��� ��� �����͸� ������
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);

        // RMS (Root Mean Square) ���
        float sum = 0f;
        foreach (float sample in samples)
        {
            sum += sample * sample;
        }
        return Mathf.Sqrt(sum / samples.Length);  // RMS �� ��ȯ
    }
}
