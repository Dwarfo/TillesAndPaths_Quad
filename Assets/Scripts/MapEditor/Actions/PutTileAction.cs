using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutTileAction : GenericAction
{
    private Dictionary<int, SO_Tile> intToTileTypes;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private float tileWidth;
    [SerializeField]
    private float tileHeight;

    public void Start()
    {
        SO_GameSettings settings = EditorManager.Instance.gameSettings;
        tilePrefab = settings.tilePrefab;
        tileWidth = settings.tileWidth;
        tileHeight = settings.tileHeight;
        parentTransform = transform;
    }
    public override void Execute(Vector2 index, ActionsArgument argument)
    {
        CreateTileArgument tileArgument = (CreateTileArgument)argument;

        GameObject tile = GameObject.Instantiate(tilePrefab);
        Tile tileScript = tile.GetComponent<Tile>();

        tileScript.transform.SetParent(parentTransform);
        tileScript.AssignTileData(tileArgument.tileInfo);
        tileScript.SetIndex(index, tileWidth, tileHeight);
        tile.name = "Tile" + tileScript.Index;
    }

    public override void Init(InitArgument args)
    {
        CreateTileInitArgument initArgs = (CreateTileInitArgument)args;
        parentTransform = initArgs.parentTransform;
        tilePrefab = initArgs.tilePrefab;
        tileWidth = initArgs.tileWidth;
        tileHeight = initArgs.tileHeight;
}

    public override string Name
    {
        get { return "PutTile"; }
    }
}

public class CreateTileArgument : ActionsArgument
{
    public SO_Tile tileInfo;
}

public class CreateTileInitArgument : InitArgument
{
    public CreateTileInitArgument(Transform parentTransform, GameObject tilePrefab, float tileWidth, float tileHeight)
    {
        this.parentTransform = parentTransform;
        this.tilePrefab = tilePrefab;
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
    }

    public Transform parentTransform;
    public GameObject tilePrefab;
    public float tileWidth;
    public float tileHeight;
}