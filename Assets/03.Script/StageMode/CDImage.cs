using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDImage : MonoBehaviour
{
    public Image cDimage; // �̹��� ������Ʈ ����
    public Sprite[] cdSprites; // ��������Ʈ �迭 ����

    void Update()
    {
        // ���� ���������� ���� �̹����� �����մϴ�.
        switch (StageModeStageManager.instance.currentStage)
        {
            case StageModeStageManager.Stage.FirstTheFirstStage:
                cDimage.sprite = cdSprites[0];
                break;
            case StageModeStageManager.Stage.FirstTheSecondStage:
                cDimage.sprite = cdSprites[1];
                break;
            case StageModeStageManager.Stage.FirstTheThirdStage:
                cDimage.sprite = cdSprites[2];
                break;
            case StageModeStageManager.Stage.FirstThefourthStage:
                cDimage.sprite = cdSprites[3];
                break;
            case StageModeStageManager.Stage.FirstThefifthStage:
                cDimage.sprite = cdSprites[4];
                break;
            case StageModeStageManager.Stage.FirstTheSixthStage:
                cDimage.sprite = cdSprites[5];
                break;
            case StageModeStageManager.Stage.FirstTheSeventhStage:
                cDimage.sprite = cdSprites[6];
                break;
            case StageModeStageManager.Stage.FirstTheEighthStage:
                cDimage.sprite = cdSprites[7];
                break;

            case StageModeStageManager.Stage.SecondTheFirstStage:
                cDimage.sprite = cdSprites[0];
                break;
            case StageModeStageManager.Stage.SecondTheSecondStage:
                cDimage.sprite = cdSprites[1];
                break;
            case StageModeStageManager.Stage.SecondTheThirdStage:
                cDimage.sprite = cdSprites[2];
                break;
            case StageModeStageManager.Stage.SecondThefourthStage:
                cDimage.sprite = cdSprites[3];
                break;
            case StageModeStageManager.Stage.SecondThefifthStage:
                cDimage.sprite = cdSprites[4];
                break;
            case StageModeStageManager.Stage.SecondTheSixthStage:
                cDimage.sprite = cdSprites[5];
                break;
            case StageModeStageManager.Stage.SecondTheSeventhStage:
                cDimage.sprite = cdSprites[6];
                break;
            case StageModeStageManager.Stage.SecondTheEighthStage:
                cDimage.sprite = cdSprites[7];
                break;

            case StageModeStageManager.Stage.ThirdTheFirstStage:
                cDimage.sprite = cdSprites[0];
                break;
            case StageModeStageManager.Stage.ThirdTheSecondStage:
                cDimage.sprite = cdSprites[1];
                break;
            case StageModeStageManager.Stage.ThirdTheThirdStage:
                cDimage.sprite = cdSprites[2];
                break;
            case StageModeStageManager.Stage.ThirdThefourthStage:
                cDimage.sprite = cdSprites[3];
                break;
            case StageModeStageManager.Stage.ThirdThefifthStage:
                cDimage.sprite = cdSprites[4];
                break;
            case StageModeStageManager.Stage.ThirdTheSixthStage:
                cDimage.sprite = cdSprites[5];
                break;
            case StageModeStageManager.Stage.ThirdTheSeventhStage:
                cDimage.sprite = cdSprites[6];
                break;
            case StageModeStageManager.Stage.ThirdTheEighthStage:
                cDimage.sprite = cdSprites[7];
                break;
            default:
                // �⺻�� ó�� (�ʿ信 ���� �߰�)
                break;
        }
    }
}
