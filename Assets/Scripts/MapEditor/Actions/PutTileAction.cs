using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutTileAction : IAction
{

    public delegate Tile PutTile(GameObject tilePrefab, SO_Tile tileData, Vector2 position);
    private Dictionary<int, SO_Tile> intToTileTypes;
    public void Execute(Vector2 index, ActionsArgument argument)
    {
        CreateTileArgument tileArgument = (CreateTileArgument)argument;

        GameObject tile = GameObject.Instantiate(tileArgument.tilePrefab);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(tileArgument.tileInfo);
        tileScript.SetIndex(tileArgument.indexArgument, tileArgument.tileWidth, tileArgument.tileHeight);
        tile.name = "Tile" + tileScript.Index;
    }

    public string Name
    {
        get { return "PutTile"; }
    }
}

public class CreateTileArgument : ActionsArgument
{
    public Transform parentTransform;
    public GameObject tilePrefab;
    public SO_Tile tileInfo;
    public float tileWidth;
    public float tileHeight;

}