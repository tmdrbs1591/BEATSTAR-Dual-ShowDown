using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> childObjectsToToggle;// Ȱ��ȭ�� ��ȯ�� �ڽ� ������Ʈ ����Ʈ

    public void OnPointerEnter(PointerEventData eventData)// ���콺 �����Ͱ� ������Ʈ�� ������ �� ȣ��Ǵ� �޼���
    {
        ToggleChildObjects(true);// �ڽ� ������Ʈ���� Ȱ��ȭ
    }

    public void OnPointerExit(PointerEventData eventData)   // ���콺 �����Ͱ� ������Ʈ���� ���� �� ȣ��Ǵ� �޼���
    {
        ToggleChildObjects(false);// �ڽ� ������Ʈ���� ��Ȱ��ȭ
    }

    private void ToggleChildObjects(bool state) // �ڽ� ������Ʈ���� Ȱ��ȭ ���¸� ��ȯ�ϴ� �޼���
    {
        foreach (GameObject child in childObjectsToToggle)
        {
            AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.3f, 1.3f), 1);    // AudioManager�� ���� ���� ���
            child.SetActive(state);// �ڽ� ������Ʈ�� Ȱ��ȭ ���¸� ����
        }
    }
}
