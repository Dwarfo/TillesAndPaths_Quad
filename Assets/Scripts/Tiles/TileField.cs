﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileField : MonoBehaviour
{
    public float tileSize;
    public GameObject tilePrefab;

    [SerializeField]
    private SO_Tile[] tiles;
    private TileTypes[][] tipowoi;
    private Dictionary<int, SO_Tile> intToTileTypes;
    private Dictionary<Vector2, Tile> vectorsToTiles;
    [SerializeField] SO_TileFieldToGen testTileField;
 
    void Start()
    {
        vectorsToTiles = new Dictionary<Vector2, Tile>();
        GenerateEncounterMap(testTileField);
        SetNeighbours();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 vec = IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), EditorManager.Instance.gameSettings.tileWidth, EditorManager.Instance.gameSettings.tileHeight);
            foreach (Tile t in vectorsToTiles[vec].Neighbours) 
            {
                Debug.Log("Neighbour:" + t.Index);
            }
        }
    }

    #region Indexing
    //Indexing, tile choosing by index and mouse position
    //---------------------------------------------------------------------------
    public static Vector2 IndexOfPosition(Vector3 mp, float w, float h)
    {
        int x = Mathf.FloorToInt(((mp.y) / (2 * h)) + ((mp.x) / (2 * w)));
        int y = Mathf.FloorToInt(((mp.y) / (2 * h)) - ((mp.x) / (2 * w)));

        return new Vector2(x, y);
    }

    public Tile GetTileByIndex(Vector2 positionIndex)
    {
        if (vectorsToTiles.ContainsKey(positionIndex))
        {
            return vectorsToTiles[positionIndex];
        }
        else
            return null;
    }
    //---------------------------------------------------------------------------
    #endregion

    #region Map_Generating
    //Map generating
    //---------------------------------------------------------------------------
    public void SetSettings(SO_GameSettings settings)
    {
        this.tilePrefab = settings.tilePrefab;

        if (settings.allTileTypes.Length != 0)
        {
            this.tiles = settings.allTileTypes;
        }
    }

    public void Process()
    {
        intToTileTypes = new Dictionary<int, SO_Tile>();

        AddTilesToDict(intToTileTypes, tiles); //Adding tiletypes to dictionary for generating map
    }
    public static void AddTilesToDict(Dictionary<int, SO_Tile> dict, SO_Tile[] tiles)
    {
        foreach (var tile in tiles)
            dict.Add((int)tile.tileType, tile);
    }

    public void DrawTilesFromMap(MapData mdata)
    {
        foreach (TileEntry entry in mdata.tiles)
        {
            var newTile = MakeTileFromEntry(entry);
            vectorsToTiles.Add(newTile.Index, newTile);
        }
    }

    public void GenerateEncounterMap(SO_TileFieldToGen tileFieldToGen)
    {
        int totalWeight = 0;

        foreach (TileToPercentage elem in tileFieldToGen.tileToPercentage)
        {
            totalWeight += elem.percentage;
        }

        int val;
        SO_Tile tileType = null;
        for (int i = 0; i < tileFieldToGen.sizeX; i++) 
        {
            for (int j = 0; j > -tileFieldToGen.sizeY; j--)
            {
                val = Random.Range(0, totalWeight);
                for (int k = 0; k < tileFieldToGen.tileToPercentage.Length; k++)
                {
                    if (val < tileFieldToGen.tileToPercentage[k].percentage)
                    {
                        tileType = tileFieldToGen.tileToPercentage[k].tileType;
                        break;
                    }
                    else
                    {
                        val -= tileFieldToGen.tileToPercentage[k].percentage;
                    }
                }

                vectorsToTiles.Add(new Vector2(i, j), MakeTile(tilePrefab, tileType, new Vector2(i, j)));
            }
        }
    }

    public Tile MakeTile(GameObject tilePrefab, SO_Tile tileData, Vector2 position)
    {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.SetParent(transform);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(tileData);
        tileScript.SetIndex(position, 1.075f, 0.62f);
        //tile.GetComponent<SpriteRenderer>().sortingOrder = -1 * (int)position.y;

        return tileScript;
    }

    public void ClearMap()
    {
        foreach (KeyValuePair<Vector2, Tile> tile in vectorsToTiles)
        {
            Destroy(tile.Value.gameObject);
        }

        vectorsToTiles = new Dictionary<Vector2, Tile>();
    }

    private Tile MakeTileFromEntry(TileEntry entry) 
    {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.SetParent(transform);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(intToTileTypes[entry.tileType]);
        //tileScript.SetIndex(new Vector2(entry.xPosition, entry.yPosition));
        tile.name = "Tile" + tileScript.Index;
        return tileScript;
    }

    public void SetNeighbours()
    {
        foreach (Tile t in vectorsToTiles.Values)
        {
            t.Neighbours.Clear();
        }

        HashSet<Tile> tilesToCheck = new HashSet<Tile>();
        HashSet<Tile> newToCheck = new HashSet<Tile>();
        HashSet<Vector2> ignore = new HashSet<Vector2>();
        tilesToCheck.Add(vectorsToTiles[new Vector2(0,0)]);

        while (tilesToCheck.Count != 0)
        {
            foreach (var tile in tilesToCheck)
            {
                AddNeighbours(newToCheck, ignore, tile, tile.Index);
            }

            tilesToCheck = new HashSet<Tile>(newToCheck);
            newToCheck = new HashSet<Tile>();
        }
    }
    private void AddNeighbours(HashSet<Tile> newToCheck, HashSet<Vector2> ignore, Tile centerTile, Vector2 ind)
    {
        Vector2[] neighbourIndexes = new Vector2[] { ind + new Vector2(0, 1) , ind + new Vector2(0, -1) ,
                ind + new Vector2(-1, 0), ind + new Vector2(1, 0)};

        //Diagonal Indexes
        Tile neighbour;


        foreach (var index in neighbourIndexes)
        {
            if (vectorsToTiles.ContainsKey(index))
            {
                if (!ignore.Contains(index))
                {
                    neighbour = vectorsToTiles[index];
                    centerTile.AddNeighbour(neighbour);
                    neighbour.AddNeighbour(centerTile);
                    newToCheck.Add(neighbour);
                }
            }
        }

        ignore.Add(centerTile.Index);
    }

    private void WriteDebugNeighbours()
    {
        foreach (var tile in vectorsToTiles.Keys)
        {
            //Debug.Log(vectorsToTiles[tile].gameObject.name + " count " + vectorsToTiles[tile].Neighbours.Count);
            string neighboursStr = "";
            foreach (var n in vectorsToTiles[tile].Neighbours)
            {
                neighboursStr += n.Index + " ";
            }
            Debug.Log("Tile " + vectorsToTiles[tile].Index + " has neighbours: " + neighboursStr);
        }
    }
    //---------------------------------------------------------------------------
    #endregion

    #region Pathfinding
    // Generating Path object
    public Path GetPath(Vector2 start, Vector2 toGo)
    {
        if (!vectorsToTiles.ContainsKey(toGo))
            return null;
        if (vectorsToTiles[toGo].IsImpassible)
            return null;

        CalculateHeuristics(vectorsToTiles[toGo]);
        Debug.Log("Outside: " + vectorsToTiles[toGo].gameObject.name + "  Num of neighbours: " + vectorsToTiles[toGo].Neighbours.Count);
        return CreatePath(vectorsToTiles[start], vectorsToTiles[toGo]);
    }

    private void CalculateHeuristics(Tile start)
    {
        foreach (var tile in vectorsToTiles.Keys)
        {
            vectorsToTiles[tile].CountHeuristics(start);
        }
    }

    private Path CreatePath(Tile start, Tile end)
    {
        Path path = new Path();
        List<Tile> toCheck = new List<Tile>();
        HashSet<Tile> ignore = new HashSet<Tile>();

        foreach (var tile in vectorsToTiles.Values)
            tile.Clear();

        Tile currentTile = start;
        toCheck.Add(currentTile);

        while (toCheck.Count != 0)
        {
            currentTile = GetTileWithLowestF(toCheck);
            if (currentTile == end)
                break;

            toCheck.Remove(currentTile);
            ignore.Add(currentTile);

            foreach (var neighbour in CheckNeighboursPath(currentTile))
            {
                if (!ignore.Contains(neighbour))
                {
                    if (!toCheck.Contains(neighbour))
                    {
                        toCheck.Add(neighbour);
                        neighbour.SetParent(currentTile);
                    }
                    else if(currentTile.G + neighbour.Difficulty < neighbour.G)
                    {
                        neighbour.SetParent(currentTile);
                    }
                }
            }
        }


        while (currentTile != start)
        {
            path.AddPath(currentTile);
            currentTile = currentTile.Parent;
        }
        path.AddPath(currentTile);
        path.ActualPath();
        return path;
    }

    private List<Tile> CheckNeighboursPath(Tile tile)
    {
        List<Tile> validNeighbours = new List<Tile>();
        foreach (Tile neighbour in tile.Neighbours)
        {
            if (!neighbour.IsImpassible)
            {
                validNeighbours.Add(neighbour);
            }
        }
        //Debug.Log("NeighboursLength: " + validNeighbours.Count);
        return validNeighbours;
    }

    private Tile GetTileWithLowestF(List<Tile> tiles)
    {
        tiles.Sort(delegate (Tile t1, Tile t2)
        {
            if (t1.F > t2.F) return 1;
            else if (t1.F < t2.F) return -1;
            else return 0;
        }
        );

        return tiles[0];
    }
    //---------------------------------------------------------------------------
    #endregion
}
