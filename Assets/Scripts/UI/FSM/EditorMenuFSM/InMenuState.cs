using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMenuState : IMenuState
{
    public bool Editable()
    {
        return false;
    }

    public void ExitFromTileChooser(EditorManager manager)
    {

    }

    public void HoverOver(EditorManager manager)
    {
        
    }

    public void MenuBlocked(EditorManager manager)
    {
        manager.GoToHoveringState();
    }

    public void MenuEnetered(EditorManager manager)
    {

    }

    public void MenuExited(EditorManager manager)
    {
        manager.GoToEditingState();
    }
}
