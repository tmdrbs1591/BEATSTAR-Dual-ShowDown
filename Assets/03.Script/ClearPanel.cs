using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    public GameObject rinaimage;      // White ĳ���� �̹���
    public GameObject rinranimage;    // Red ĳ���� �̹���
    public GameObject seonIimage;     // Blue ĳ���� �̹���
    public GameObject sparkimage;     // Green ĳ���� �̹���


    public string scenceName = "StageMode";
    void OnEnable()
    {
        StartCoroutine(CameraShakes()); // ī�޶� ��鸲 ȿ���� �����ϴ� �ڷ�ƾ�� ����
        SetCharacterImage(DataManager.instance.currentCharater);
    }


    private Dictionary<Character, GameObject> characterImages;

    void Awake()
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


    IEnumerator CameraShakes()
    {
        yield return new WaitForSeconds(1.45f);// 1.45�� �Ŀ�
        CameraShake.instance.Shake();// ī�޶� �����ִ� Shake �޼��带 ȣ��/
    }
}
