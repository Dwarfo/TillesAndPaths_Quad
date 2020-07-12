using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorEditingState : EditorAbstractState
{
    public override void EnterState(EditorFSM editorFsm) 
    {

    }
    public override void MoveToEditorState(EditorFSM editorFsm)
    {
        Debug.Log("Already in editing");
    }

    public override void MoveToLoadMenu(EditorFSM editorFsm)
    {

    }

    public override void MoveToMainMenu(EditorFSM editorFsm)
    {

    }

    public override void MoveToSaveMenu(EditorFSM editorFsm)
    {

    }
}
