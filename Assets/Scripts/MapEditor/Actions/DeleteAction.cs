using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAction : IAction
{
    public void Execute(Vector2 index, ActionsArgument argument)
    {
        DeleteArgument deleteArg = (DeleteArgument)argument;
        deleteArg.tileGO.SetActive(false);
    }

    public string Name
    {
        get { return "DeleteTile"; }
    }
}

public class DeleteArgument : ActionsArgument
{
    public GameObject tileGO;
}