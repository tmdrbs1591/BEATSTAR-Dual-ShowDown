using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectDebug : MonoBehaviour
{

    void Update()
    {
     
        // 현재 선택된 UI 요소를 가져옴
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        // 선택된 UI 요소가 있을 때만 처리
        if (selectedObject != null)
        {
            StageModeStageManager.Stage newStage = StageModeStageManager.Stage.FirstTheFirstStage;

            // 버튼의 이름에 따라 스테이지 설정
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

                case "Stage4-1": newStage = StageModeStageManager.Stage.FourthTheFirstStage; break;
                case "Stage4-2": newStage = StageModeStageManager.Stage.FourthTheSecondStage; break;
                case "Stage4-3": newStage = StageModeStageManager.Stage.FourthTheThirdStage; break;
                case "Stage4-4": newStage = StageModeStageManager.Stage.FourthThefourthStage; break;
                case "Stage4-5": newStage = StageModeStageManager.Stage.FourthThefifthStage; break;
                case "Stage4-6": newStage = StageModeStageManager.Stage.FourthTheSixthStage; break;
                case "Stage4-7": newStage = StageModeStageManager.Stage.FourthTheSeventhStage; break;
                case "Stage4-8": newStage = StageModeStageManager.Stage.FourthTheEighthStage; break;

                case "Stage5-1": newStage = StageModeStageManager.Stage.FifthTheFirstStage; break;
                case "Stage5-2": newStage = StageModeStageManager.Stage.FifthTheSecondStage; break;
                case "Stage5-3": newStage = StageModeStageManager.Stage.FifthTheThirdStage; break;
                case "Stage5-4": newStage = StageModeStageManager.Stage.FifthTheFourthStage; break;
                case "Stage5-5": newStage = StageModeStageManager.Stage.FifthTheFifthStage; break;
                case "Stage5-6": newStage = StageModeStageManager.Stage.FifthTheFSixthtage; break;
                case "Stage5-7": newStage = StageModeStageManager.Stage.FifthTheSeventhStage; break;
                case "Stage5-8": newStage = StageModeStageManager.Stage.FifthTheEighthStage; break;
                default:
                    newStage = StageModeStageManager.Stage.Main; 
                    break;
            }
           
            // 스테이지 변경
            StageModeStageManager.instance.currentStage = newStage;
        }
    }
}
