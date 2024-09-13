using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotWeenAlpha : MonoBehaviour
{
    public Image image; // ���İ��� ������ Image ������Ʈ
    public float startAlpha = 0f; // ���� ���İ� (0.0�� ���� ����, 1.0�� ���� ������)
    public float targetAlpha = 1f; // ��ǥ ���İ� (0.0���� 1.0����)
    public float duration = 2f; // �ִϸ��̼� �ð�

    void Start()
    {
        image = GetComponent<Image>(); // �� ��ũ��Ʈ�� �پ� �ִ� ��ü�� Image ������Ʈ ��������

      
    }
    private void OnEnable()
    {
        if (image != null)
        {
            // �ʱ� ���İ� ����
            Color currentColor = image.color;
            currentColor.a = startAlpha;
            image.color = currentColor;

            // ��Ʈ���� �̿��Ͽ� ���İ��� ������ ����
            image.DOFade(targetAlpha, duration);
        }
    }
}
