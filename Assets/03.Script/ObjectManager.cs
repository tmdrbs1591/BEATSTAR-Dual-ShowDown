using System.Collections;
using UnityEngine;

public class ObjectManager : MonoBehaviour // 코드 삭제 예정이라 주석 없음
{
    public GameObject objParent;


    public GameObject go1Prefab;
    public GameObject go2Prefab;
    public GameObject go3Prefab;


    GameObject[] go1;
    GameObject[] go2;
    GameObject[] go3;


    GameObject[] targetPool;

    void Awake()
    {
        go1 = new GameObject[150];
        go2 = new GameObject[150];
        go3 = new GameObject[150];


        objParent = new GameObject();
        objParent.name = "Obj";
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < go1.Length; i++)
        {
            go1[i] = Instantiate(go1Prefab, objParent.transform);
            go1[i].SetActive(false);
        }
        for (int i = 0; i < go2.Length; i++)
        {
            go2[i] = Instantiate(go2Prefab, objParent.transform);
            go2[i].SetActive(false);
        }
        for (int i = 0; i < go3.Length; i++)
        {
            go3[i] = Instantiate(go3Prefab, objParent.transform);
            go3[i].SetActive(false);
        }

    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "go1":
                targetPool = go1;
                break;
            case "go2":
                targetPool = go2;
                break;
            case "go3":
                targetPool = go3;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                StartCoroutine(DisableSlash(targetPool[i], type));
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    IEnumerator DisableSlash(GameObject slashObj, string type)
    {
        float disableTime = 0f;

        switch (type)
        {
            case "go1":
                disableTime = 2f;
                break;
            case "go2":
                disableTime = 2f;
                break;
            case "go3":
                disableTime = 2f;
                break;
        }

        yield return new WaitForSeconds(disableTime);

        if (slashObj.activeSelf)
        {
            slashObj.SetActive(false);
        }
    }

    public GameObject[] GetPool(string type)
    {
        return targetPool;
    }
}
