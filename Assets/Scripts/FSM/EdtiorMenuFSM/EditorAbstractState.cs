using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EditorAbstractState
{
    public abstract void EnterState(EditorFSM editorFsm);
    public abstract void MoveToEditorState(EditorFSM editorFsm);
    public abstract void MoveToMainMenu(EditorFSM editorFsm);
    public abstract void MoveToLoadMenu(EditorFSM editorFsm);
    public abstract void MoveToSaveMenu(EditorFSM editorFsm);
}
