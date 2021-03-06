﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    public GameObject tilePrefab;
    public List<Tile> duplicatesToDestroy;

    private Vector2 currentMousePositionIndex;
    private Vector2 lastDrawnIndex;

    [SerializeField]
    private List<GenericAction> allActions;
    [SerializeField]
    private TileTypes currentTileType;
    private float tileWidth;
    private float tileHeight;
    private Dictionary<Vector2, Tile> indexToTileDict;
    private Dictionary<int, SO_Tile> intToTileTypes;
    private Dictionary<string, IAction> nameToAction;

    public TileField tileField;

    //actions functionality
    private ActionsArgument actionsArgument;
    private IAction currentAction;

    void Start()
    {
        lastDrawnIndex      = new Vector2Int(0, 0);
        indexToTileDict     = new Dictionary<Vector2, Tile>();
        duplicatesToDestroy = new List<Tile>();
        nameToAction = new Dictionary<string, IAction>();

        FillActionDict();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EditorManager.Instance.currentState.Editable())
            return;

        currentMousePositionIndex = TileField.IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), tileWidth, tileHeight);

        if (lastDrawnIndex != currentMousePositionIndex) 
        {
            lastDrawnIndex = currentMousePositionIndex;
            //redo by event
            UIManager.Instance.lineDrawer.DrawIsoRectangle(lastDrawnIndex, tileWidth, tileHeight);
            if (Input.GetMouseButton(0))
            {

                //actionsArgument.indexArgument = currentMousePositionIndex;
                //currentAction.Execute(currentMousePositionIndex, actionsArgument);
                Debug.Log("Tile coord:" + currentMousePositionIndex);
            }

        }

        if (Input.GetMouseButtonDown(0)) 
        {
            //actionsArgument.indexArgument = currentMousePositionIndex;
            //currentAction.Execute(currentMousePositionIndex, actionsArgument);
            Debug.Log("Tile coord:" + currentMousePositionIndex);
        }
        if (Input.GetMouseButton(1)) 
        {
            Debug.Log(currentMousePositionIndex);
            Debug.Log("Real mouse pos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void FillActionDict() 
    {
        foreach(GenericAction action in allActions)
        {
            nameToAction.Add(action.Name, action);
        }
    }

    public void PutTileInField(Vector2 position, int tileType) 
    {
        if (tileType == 0) 
        {
            return;
        }

        Tile newTile = MakeTile(tilePrefab, intToTileTypes[tileType], position);
        //Replace old tile if it already consists on 'position' index
        if (indexToTileDict.ContainsKey(position))
        {
            duplicatesToDestroy.Add(indexToTileDict[position]);
            indexToTileDict[position].gameObject.SetActive(false);
        }

        indexToTileDict[position] = newTile;
    }

    public void SetSettings(SO_GameSettings gameSettings) 
    {
        this.tileWidth = gameSettings.tileWidth;
        this.tileHeight = gameSettings.tileHeight;
        intToTileTypes = new Dictionary<int, SO_Tile>();

        TileField.AddTilesToDict(intToTileTypes, gameSettings.allTileTypes);
    }
    public MapData GetEditedMapData() 
    {
        List<TileEntry> entryList = new List<TileEntry>();
        foreach (KeyValuePair<Vector2, Tile> tile in indexToTileDict)
        {
            //tile.Value.SetIndex(tile.Value.Index.x - minX, tile.Value.Index.y - minY);
            TileEntry entry = new TileEntry();
            entry.xPosition = tile.Value.Index.x;
            entry.yPosition = tile.Value.Index.y;
            entry.tileType = (int)tile.Value.Type;
            entryList.Add(entry);
        }

        MapData md = new MapData();
        md.tiles = entryList;

        return md;
    }

    private Tile MakeTile(GameObject tilePrefab, SO_Tile tileData, Vector2 position)
    {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.SetParent(transform);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(tileData);
        tileScript.SetIndex(position, tileWidth, tileHeight);
        tile.name = "Tile" + tileScript.Index;
        //tile.GetComponent<SpriteRenderer>().sortingOrder = -1 * (int)position.y;

        return tileScript;
    }

    public void SetCurrentTileType(TileTypes tileType) 
    {
        currentTileType = tileType;
        CreateTileArgument tileArgument = new CreateTileArgument();
        tileArgument.tileInfo = intToTileTypes[(int)tileType];

        currentAction = nameToAction["PutTile"];
        actionsArgument = tileArgument;
}

    public void ClearIndexDrawer() 
    {
        UIManager.Instance.lineDrawer.ClearLine();
    }
}
