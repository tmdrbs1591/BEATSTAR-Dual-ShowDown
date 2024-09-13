using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpText : MonoBehaviour
{
    public PlaayerController thePlayerController; // 플레이어 컨트롤러
    public TMP_Text hpText;//표시할 hp 텍스트
    void FixedUpdate()
    {
        if (thePlayerController != null)
        {
            thePlayerController = FindObjectOfType<PlaayerController>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = thePlayerController.CurHP.ToString();
    }
}
