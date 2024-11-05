using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectBtn : MonoBehaviour
{
    Animator anim;
    [SerializeField] Character currentchar;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.instance.currentCharater == currentchar)
        {
            anim.SetBool("isSelect", true);
        }
        else
        {
            anim.SetBool("isSelect", false);
        }
    }
}
