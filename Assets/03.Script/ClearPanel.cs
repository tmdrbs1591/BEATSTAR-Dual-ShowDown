using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    public GameObject rinaimage;      // White 캐릭터 이미지
    public GameObject rinranimage;    // Red 캐릭터 이미지
    public GameObject seonIimage;     // Blue 캐릭터 이미지
    public GameObject sparkimage;     // Green 캐릭터 이미지


    public string scenceName = "StageMode";
    void OnEnable()
    {
        StartCoroutine(CameraShakes()); // 카메라 흔들림 효과를 시작하는 코루틴을 실행
        SetCharacterImage(DataManager.instance.currentCharater);
    }


    private Dictionary<Character, GameObject> characterImages;

    void Awake()
    {
        // 캐릭터와 이미지 매핑 설정
        characterImages = new Dictionary<Character, GameObject>
        {
            { Character.White, rinaimage },
            { Character.Red, rinranimage },
            { Character.Blue, seonIimage },
            { Character.Green, sparkimage }
        };
    }

    void Update()
    {
    
    }

    void SetCharacterImage(Character currentCharacter)
    {
        // 모든 이미지 비활성화
        foreach (var image in characterImages.Values)
        {
            image.SetActive(false);
        }

        // 현재 캐릭터 이미지만 활성화
        if (characterImages.TryGetValue(currentCharacter, out var activeImage))
        {
            activeImage.SetActive(true);
        }
    }


    IEnumerator CameraShakes()
    {
        yield return new WaitForSeconds(1.45f);// 1.45초 후에
        CameraShake.instance.Shake();// 카메라를 흔들어주는 Shake 메서드를 호출/
    }
}
