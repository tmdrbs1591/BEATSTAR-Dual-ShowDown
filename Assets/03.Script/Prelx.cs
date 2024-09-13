using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [Header("Layer Settings")]
    public float[] layerSpeed = new float[7];// �� ���̾��� ��ũ�� �ӵ� �迭
    public GameObject[] layerObjects = new GameObject[7];// �� ���̾��� ���� ������Ʈ �迭
        
    private float[] startPositions = new float[7];// �� ���̾��� �ʱ� ��ġ �迭
    private float[] boundsSizes = new float[7];// �� ���̾��� �ٿ�� ������ �迭

    public float MapSpeed = 1;// ���� ��ũ�� �ӵ� ����

    void Start()
    {
        // ��� ���̾ ���� �ʱ� ��ġ�� �ٿ�� ������ ���
        for (int i = 0; i < layerObjects.Length; i++)
        {
            startPositions[i] = layerObjects[i].transform.position.x;
            boundsSizes[i] = layerObjects[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    void FixedUpdate()
    {
        // �� ���̾ �׿� �´� �ӵ��� ���� ��ü �ӵ��� ���� �̵���Ŵ
        for (int i = 0; i < layerObjects.Length; i++)
        {
            float distance = Time.fixedDeltaTime * layerSpeed[i] * MapSpeed;// �̵��� �Ÿ� ���
            layerObjects[i].transform.position += Vector3.left * distance;// ���� �������� �̵�

            // ���̾ �ʱ� ��ġ�� �Ѿ���� Ȯ��
            if (layerObjects[i].transform.position.x < startPositions[i] - boundsSizes[i])
            {
                // �Ѿ�ٸ� �ݴ������� �̵���Ŵ
                layerObjects[i].transform.position += Vector3.right * (2 * boundsSizes[i]);
            }
        }
    }
}
