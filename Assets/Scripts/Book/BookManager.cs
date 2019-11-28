using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public BlocNoteManager blocNote;
    public Photo.UIPhoto UIPhoto;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            blocNote.ToggleBlocNote();
        else if (Input.GetKeyDown(KeyCode.A))
            UIPhoto.ToggleUI();

    }
}
