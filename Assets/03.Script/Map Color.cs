using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] mapSprite;
    [SerializeField] SpriteRenderer[] GroundSprite;
    public Color mapcolors; // ¸Ê »ö»ó 
    public Color Groundcolors; // ¹Ù´Ú »ö»ó 
    public float count;

    void Start()
    {
        StartCoroutine(ColorChange());
    }

  
    void Update()
    {
        
    }
    IEnumerator ColorChange() // ¸Ê »ö±ò ¹Ù²Ù±â
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
