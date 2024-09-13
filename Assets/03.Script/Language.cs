using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class Language : MonoBehaviour
{

    private void Start()
    {
        //UserLocalization(0);
    }
    public void UserLocalization(int index) // 사용자가 선택한 언어 인덱스를 받아 해당 언어로 설정하는 메서드
    {
        // AvailableLocales.Locales는 사용 가능한 모든 로케일 목록을 제공
        // SelectedLocale은 현재 선택된 로케일을 나타냄
        LocalizationSettings.SelectedLocale  =
            LocalizationSettings.AvailableLocales.Locales[index];
    }
}
