using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : MonoBehaviour
{
    [SerializeField]
    float speed = 10f; // ������ �̵� �ӵ�

    void Update()
    {

        transform.localPosition += Vector3.left * speed * Time.deltaTime;
    }
}
