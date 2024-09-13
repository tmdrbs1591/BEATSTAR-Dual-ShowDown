using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] charObject;
    public GameObject player;

    void Awake()
    {
        player = Instantiate(charObject[(int)DataManager.instance.currentCharater]);
        player.transform.position = transform.position;

        player.SetActive(true);
    }

    void Update()
    {
    }
}
