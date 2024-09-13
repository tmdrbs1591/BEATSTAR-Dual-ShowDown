using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class SecondStage : ReFirstStage //기본 스테이지 값들 상속
{
    public GameObject powerEffect;
    public GameObject powerMap;

    void Start() //상속받은 스크립트 주석과 같다
    {
        Song.Stop();
        thecomboManager = FindObjectOfType<ComboManager>(); //상속받은 스크립트 주석과 같다
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();

        string path = Application.dataPath + "/Songs/" + DataManager.instance.songPath + ".json";
        using (StreamReader reader = new StreamReader(path))
        {
            notemap = JsonUtility.FromJson<EditorManager.SerializableList<NoteInfo>>(reader.ReadToEnd()).list;
        }


        foreach (NoteInfo e in notemap)
        {
            allNotes++;
            maxNotes++;
            StartCoroutine(QueueToSpawn(e)); // 빋아온다
        }
    }

    void FixedUpdate()
    {
        if (thePlayerController != null)
        {
            thePlayerController = FindObjectOfType<PlaayerController>();

        }
        if (allNotes <= 0)
        {
            StartCoroutine(Clear(5f));
        }
        if (allNotes == maxNotes - 112)
        {
            powerMap.SetActive(true);
            map.MapSpeed *= 3;
        }

        if (allNotes == maxNotes - 154)
        {
            StartCoroutine(EffectTrue(0.45f, powerEffect));
        }
        if (allNotes == maxNotes - 155)
        {
            map.MapSpeed *= 3;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(maxNotes - allNotes);
        }
        double beatInterval = 60d / bpm;

        currentTime += Time.deltaTime;
    }
}