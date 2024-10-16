using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseSongPlay : MonoBehaviour
{

    private void OnDisable()
    {
        TitleSobf.instance.titleSongPlay();
    }
}
