using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    public string noteKey;
    UnityEngine.UI.Image noteImage;

    void OnEnable()
    {
        if (noteImage   == null)
        noteImage = GetComponent<UnityEngine.UI.Image>();

        noteImage.enabled = true;
    }
    private void Start()
    {
    }
    void Update()
    {
        transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }
    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }

}
