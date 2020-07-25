using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingState : IMenuState
{
    public bool Editable()
    {
        return true;
    }

    public void ExitFromTileChooser(EditorManager manager)
    {

    }

    public void HoverOver(EditorManager manager)
    {
        manager.GoToHoveringState();
    }

    public void MenuEnetered(EditorManager manager)
    {
        manager.GoToInMenuState();
    }

    public void MenuExited(EditorManager manager)
    {

    }

    public void MenuBlocked(EditorManager manager)
    {

    }
}