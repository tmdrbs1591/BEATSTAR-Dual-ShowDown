using UnityEngine;
using DG.Tweening;

public class SkillEffectAnimation : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveTime = 1f;
    public Ease easeType = Ease.Linear;

    void Start()
    {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition1 = originalPosition + new Vector3(moveDistance, moveDistance, 0);
        Vector3 targetPosition2 = originalPosition + new Vector3(moveDistance, -moveDistance, 0);

        // ������ ���� �̵�
        transform.DOMove(targetPosition1, moveTime)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                // ������ �̵�
                transform.DOMove(targetPosition2, moveTime)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        // �ٽ� ���� ��ġ�� ���ƿ���
                        transform.DOMove(originalPosition, moveTime)
                            .SetEase(easeType);
                    });
            });
    }
}
