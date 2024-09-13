using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ThirdStage : ReFirstStage // 상속받은 것과 주석 같음
{
    void Start()
    {
        Song.Stop();
        thecomboManager = FindObjectOfType<ComboManager>();
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
            StartCoroutine(Clear(3f));
        }
      

        double beatInterval = 60d / bpm;

        currentTime += Time.deltaTime;
    }
}