using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageModeStage1 : ReFirstStage
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
            StartCoroutine(QueueToSpawn(e)); // •±¾Æ¿Â´Ù
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

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(maxNotes - allNotes);
        }
        double beatInterval = 60d / bpm;

        currentTime += Time.deltaTime;
    }
}
