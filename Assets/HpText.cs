using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpText : MonoBehaviour
{
    public PlaayerController thePlayerController; // �÷��̾� ��Ʈ�ѷ�
    public TMP_Text hpText;//ǥ���� hp �ؽ�Ʈ
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
