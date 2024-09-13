using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [Header("Layer Settings")]
    public float[] layerSpeed = new float[7];// 각 레이어의 스크롤 속도 배열
    public GameObject[] layerObjects = new GameObject[7];// 각 레이어의 게임 오브젝트 배열
        
    private float[] startPositions = new float[7];// 각 레이어의 초기 위치 배열
    private float[] boundsSizes = new float[7];// 각 레이어의 바운드 사이즈 배열

    public float MapSpeed = 1;// 맵의 스크롤 속도 설정

    void Start()
    {
        // 모든 레이어에 대해 초기 위치와 바운드 사이즈 계산
        for (int i = 0; i < layerObjects.Length; i++)
        {
            startPositions[i] = layerObjects[i].transform.position.x;
            boundsSizes[i] = layerObjects[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    void FixedUpdate()
    {
        // 각 레이어를 그에 맞는 속도와 맵의 전체 속도에 따라 이동시킴
        for (int i = 0; i < layerObjects.Length; i++)
        {
            float distance = Time.fixedDeltaTime * layerSpeed[i] * MapSpeed;// 이동할 거리 계산
            layerObjects[i].transform.position += Vector3.left * distance;// 왼쪽 방향으로 이동

            // 레이어가 초기 위치를 넘어섰는지 확인
            if (layerObjects[i].transform.position.x < startPositions[i] - boundsSizes[i])
            {
                // 넘어섰다면 반대쪽으로 이동시킴
                layerObjects[i].transform.position += Vector3.right * (2 * boundsSizes[i]);
            }
        }
    }
}
