using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnim : MonoBehaviour 
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        
    }
    public void Right()
    {
        anim.SetTrigger("Right"); // 오른쪽 애니메이션
    }
    public void Left()
    {
        anim.SetTrigger("Left");// 왼쪽 애니메이션
    }
}
