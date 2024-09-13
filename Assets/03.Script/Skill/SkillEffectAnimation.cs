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

        // 오른쪽 위로 이동
        transform.DOMove(targetPosition1, moveTime)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                // 밑으로 이동
                transform.DOMove(targetPosition2, moveTime)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        // 다시 원래 위치로 돌아오기
                        transform.DOMove(originalPosition, moveTime)
                            .SetEase(easeType);
                    });
            });
    }
}
