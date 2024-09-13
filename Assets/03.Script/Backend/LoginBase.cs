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
    /// �޼��� ����, InputField ���� �ʱ�ȭ
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
    /// �Ű������� �ִ� ������ ���
    /// </summary>
    /// <param name="msg"></param>
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }
    /// <summary>
    ///  �Է¿������ִ� inputdied �� ���� ����
    ///  ������ ���� �޼��� ���
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
    /// �ʵ尪�� ����ִ��� Ȯ�� image:�ʵ�  field ���� reult ��µ� �Ĥ���
    /// </summary>
    protected bool IsFieldDataEmpty(Image image,string field,string result)
    {
        if (field.Trim().Equals(""))
        {
            GudieForIncorrectlyEnteredData(image, $"\"{result}\"�ʵ带 ä���ּ���.");

            return true;
        }
        return false;
    }
}
