using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> childObjectsToToggle;// 활성화를 전환할 자식 오브젝트 리스트

    public void OnPointerEnter(PointerEventData eventData)// 마우스 포인터가 오브젝트에 진입할 때 호출되는 메서드
    {
        ToggleChildObjects(true);// 자식 오브젝트들을 활성화
    }

    public void OnPointerExit(PointerEventData eventData)   // 마우스 포인터가 오브젝트에서 나갈 때 호출되는 메서드
    {
        ToggleChildObjects(false);// 자식 오브젝트들을 비활성화
    }

    private void ToggleChildObjects(bool state) // 자식 오브젝트들의 활성화 상태를 전환하는 메서드
    {
        foreach (GameObject child in childObjectsToToggle)
        {
            AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1.3f, 1.3f), 1);    // AudioManager를 통해 사운드 재생
            child.SetActive(state);// 자식 오브젝트의 활성화 상태를 설정
        }
    }
}
