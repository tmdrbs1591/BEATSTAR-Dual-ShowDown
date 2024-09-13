using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectDebug : MonoBehaviour
{

    void Update()
    {
     
        // ���� ���õ� UI ��Ҹ� ������
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        // ���õ� UI ��Ұ� ���� ���� ó��
        if (selectedObject != null)
        {
            StageModeStageManager.Stage newStage = StageModeStageManager.Stage.FirstTheFirstStage;

            // ��ư�� �̸��� ���� �������� ����
            switch (selectedObject.name)
            {
                case "Stage1-1": newStage = StageModeStageManager.Stage.FirstTheFirstStage; break;
                case "Stage1-2": newStage = StageModeStageManager.Stage.FirstTheSecondStage; break;
                case "Stage1-3": newStage = StageModeStageManager.Stage.FirstTheThirdStage; break;
                case "Stage1-4": newStage = StageModeStageManager.Stage.FirstThefourthStage; break;
                case "Stage1-5": newStage = StageModeStageManager.Stage.FirstThefifthStage; break;
                case "Stage1-6": newStage = StageModeStageManager.Stage.FirstTheSixthStage; break;
                case "Stage1-7": newStage = StageModeStageManager.Stage.FirstTheSeventhStage; break;
                case "Stage1-8": newStage = StageModeStageManager.Stage.FirstTheEighthStage; break;

                case "Stage2-1": newStage = StageModeStageManager.Stage.SecondTheFirstStage; break;
                case "Stage2-2": newStage = StageModeStageManager.Stage.SecondTheSecondStage; break;
                case "Stage2-3": newStage = StageModeStageManager.Stage.SecondTheThirdStage; break;
                case "Stage2-4": newStage = StageModeStageManager.Stage.SecondThefourthStage; break;
                case "Stage2-5": newStage = StageModeStageManager.Stage.SecondThefifthStage; break;
                case "Stage2-6": newStage = StageModeStageManager.Stage.SecondTheSixthStage; break;
                case "Stage2-7": newStage = StageModeStageManager.Stage.SecondTheSeventhStage; break;
                case "Stage2-8": newStage = StageModeStageManager.Stage.SecondTheEighthStage; break;

                case "Stage3-1": newStage = StageModeStageManager.Stage.ThirdTheFirstStage; break;
                case "Stage3-2": newStage = StageModeStageManager.Stage.ThirdTheSecondStage; break;
                case "Stage3-3": newStage = StageModeStageManager.Stage.ThirdTheThirdStage; break;
                case "Stage3-4": newStage = StageModeStageManager.Stage.ThirdThefourthStage; break;
                case "Stage3-5": newStage = StageModeStageManager.Stage.ThirdThefifthStage; break;
                case "Stage3-6": newStage = StageModeStageManager.Stage.ThirdTheSixthStage; break;
                case "Stage3-7": newStage = StageModeStageManager.Stage.ThirdTheSeventhStage; break;
                case "Stage3-8": newStage = StageModeStageManager.Stage.ThirdTheEighthStage; break;
                default:
                    newStage = StageModeStageManager.Stage.Main; 
                    break;
            }
           
            // �������� ����
            StageModeStageManager.instance.currentStage = newStage;
        }
    }
}
