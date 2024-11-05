using System.Collections.Generic;
using UnityEngine;

public class CurrentCharImage : MonoBehaviour
{
    public GameObject rinaimage;      // White ĳ���� �̹���
    public GameObject rinranimage;    // Red ĳ���� �̹���
    public GameObject seonIimage;     // Blue ĳ���� �̹���
    public GameObject sparkimage;     // Green ĳ���� �̹���

    private Dictionary<Character, GameObject> characterImages;

    void Start()
    {
        // ĳ���Ϳ� �̹��� ���� ����
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
        // ��� �̹��� ��Ȱ��ȭ
        foreach (var image in characterImages.Values)
        {
            image.SetActive(false);
        }

        // ���� ĳ���� �̹����� Ȱ��ȭ
        if (characterImages.TryGetValue(currentCharacter, out var activeImage))
        {
            activeImage.SetActive(true);
        }
    }
}
