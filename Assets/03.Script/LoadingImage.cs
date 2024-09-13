using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    [SerializeField]
    Image[] backGroundImage; // 이미지 배열

    void Start()
    {
        // 배열에 이미지가 할당되어 있는지 확인
        if (backGroundImage.Length > 0)
        {
            // 배열 범위 내에서 임의의 인덱스 선택 
            int randomIndex = Random.Range(0, backGroundImage.Length);

            // 선택된 이미지 활성화
            backGroundImage[randomIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No images assigned to backGroundImage array.");// 배열에 이미지가 할당되지 않았을 경우 경고 출력
        }
    }

    void Update()
    {
        // You can add update logic here if needed
    }
}
