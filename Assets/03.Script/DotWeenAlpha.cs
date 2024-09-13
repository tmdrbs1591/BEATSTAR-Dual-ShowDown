using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotWeenAlpha : MonoBehaviour
{
    public Image image; // 알파값을 조정할 Image 컴포넌트
    public float startAlpha = 0f; // 시작 알파값 (0.0은 완전 투명, 1.0은 완전 불투명)
    public float targetAlpha = 1f; // 목표 알파값 (0.0부터 1.0까지)
    public float duration = 2f; // 애니메이션 시간

    void Start()
    {
        image = GetComponent<Image>(); // 이 스크립트가 붙어 있는 객체의 Image 컴포넌트 가져오기

      
    }
    private void OnEnable()
    {
        if (image != null)
        {
            // 초기 알파값 설정
            Color currentColor = image.color;
            currentColor.a = startAlpha;
            image.color = currentColor;

            // 다트윈을 이용하여 알파값을 서서히 조정
            image.DOFade(targetAlpha, duration);
        }
    }
}
