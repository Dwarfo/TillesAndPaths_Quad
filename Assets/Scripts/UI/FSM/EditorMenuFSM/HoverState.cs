using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverState : IMenuState
{
    public bool Editable()
    {
        return false;
    }

    public void ExitFromTileChooser(EditorManager manager)
    {
        manager.GoToEditingState();
    }

    public void HoverOver(EditorManager manager)
    {

    }

    public void MenuBlocked(EditorManager manager)
    {
        
    }

    public void MenuEnetered(EditorManager manager)
    {

    }

    public void MenuExited(EditorManager manager)
    {
        
    }
}
