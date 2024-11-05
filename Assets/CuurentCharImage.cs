using System.Collections.Generic;
using UnityEngine;

public class CurrentCharImage : MonoBehaviour
{
    public GameObject rinaimage;      // White 캐릭터 이미지
    public GameObject rinranimage;    // Red 캐릭터 이미지
    public GameObject seonIimage;     // Blue 캐릭터 이미지
    public GameObject sparkimage;     // Green 캐릭터 이미지

    private Dictionary<Character, GameObject> characterImages;

    void Start()
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
        SetCharacterImage(DataManager.instance.currentCharater);
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
}
