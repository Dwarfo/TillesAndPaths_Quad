using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAction : GenericAction
{
    [SerializeField]
    private TileField tileField;
    public override void Execute(Vector2 index, ActionsArgument argument)
    {
        Tile tileScript = tileField.GetTileByIndex(index);
        tileScript.gameObject.SetActive(false);
    }

    public void SetTileField(TileField tf) 
    {
        tileField = tf;
    }

    public override void Init(InitArgument args)
    {
        DeleteInitArgument deleteArgs = (DeleteInitArgument)args;
        this.tileField = deleteArgs.tf;
    }

    public override string Name
    {
        get { return "DeleteTile"; }
    }
}

public class DeleteArgument : ActionsArgument
{
    public GameObject tileGO;
}

public class DeleteInitArgument : InitArgument 
{
    public DeleteInitArgument(TileField tf) 
    {
        this.tf = tf;
    }

    public TileField tf;
}