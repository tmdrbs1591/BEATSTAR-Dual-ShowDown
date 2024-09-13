using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject FadeOut;
    void Start()
    {
        FadeOut.SetActive(true);
    }


    void Update()
    {

    }
}
