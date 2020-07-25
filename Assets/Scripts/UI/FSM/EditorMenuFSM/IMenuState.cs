using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuState
{
    void HoverOver(EditorManager manager);
    void ExitFromTileChooser(EditorManager manager);
    void MenuEnetered(EditorManager manager);
    void MenuExited(EditorManager manager);
    void MenuBlocked(EditorManager manager);
    bool Editable();
}
