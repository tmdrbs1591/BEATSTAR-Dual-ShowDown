using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] mapSprite;
    [SerializeField] SpriteRenderer[] GroundSprite;
    public Color mapcolors; // �� ���� 
    public Color Groundcolors; // �ٴ� ���� 
    public float count;

    void Start()
    {
        StartCoroutine(ColorChange());
    }

  
    void Update()
    {
        
    }
    IEnumerator ColorChange() // �� ���� �ٲٱ�
    {
        yield return new WaitForSeconds(count);
        for (int i = 0; i < mapSprite.Length; i++)
        {
            mapSprite[i].color = mapcolors;
        }
        for (int i = 0; i < GroundSprite.Length; i++)
        {
            GroundSprite[i].color = Groundcolors;
        }
    }
}
