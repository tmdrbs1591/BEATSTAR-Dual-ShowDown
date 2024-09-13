using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditNote : MonoBehaviour
{
    public NoteInfo ei;
    [SerializeField] EditorManager em;

    public void Sart(string _note, float _timing)
    {
        ei.note = _note;
        ei.timing = _timing;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.up* transform.position.y + Vector3.right * ((ei.timing - em.time) * 10);
    }
}
