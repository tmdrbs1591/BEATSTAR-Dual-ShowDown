using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class LoginBase : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textMessage;
    
    /// <summary>
    /// 메세지 내용, InputField 색상 초기화
    /// </summary>
    /// <param name="images"></param>
    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }
    /// <summary>
    /// 매개변수에 있는 내용을 출력
    /// </summary>
    /// <param name="msg"></param>
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }
    /// <summary>
    ///  입력오류가있는 inputdied 의 색상 변경
    ///  오류에 대한 메세지 출력
    /// </summary>
    /// <param name="image"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    protected void GudieForIncorrectlyEnteredData(Image image,string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;

    }
    /// <summary>
    /// 필드값이 비어있는지 확인 image:필드  field 내용 reult 출력될 냐ㅐ용
    /// </summary>
    protected bool IsFieldDataEmpty(Image image,string field,string result)
    {
        if (field.Trim().Equals(""))
        {
            GudieForIncorrectlyEnteredData(image, $"\"{result}\"필드를 채워주세요.");

            return true;
        }
        return false;
    }
}
