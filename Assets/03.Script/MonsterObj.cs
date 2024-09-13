using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : MonoBehaviour
{
    [SerializeField]
    float speed = 10f; // 몬스터의 이동 속도

    void Update()
    {

        transform.localPosition += Vector3.left * speed * Time.deltaTime;
    }
}
