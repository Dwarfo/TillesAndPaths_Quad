using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SO_Tile tile;
    public bool invalid = false;

    private Vector2 index;
    [SerializeField]
    private List<Tile> neighbours;
    private float heuristic = 0;
    private int g = 0;
    private float f = 0;
    private Tile parent;
    [SerializeField]
    public IPickable item;

    public float H { get { return heuristic; } }
    public int G { get { return g; } }
    public float F { get { return f; } }
    public Tile Parent { get { return parent; } }

    public List<Tile> Neighbours { get { return neighbours; } }

    public Vector2 Index { get { return index; } }

    public void AssignTileData(SO_Tile tileSO)
    {
        this.tile = tileSO;
        neighbours = new List<Tile>();
        transform.localScale = new Vector3(tileSO.xSizing, tileSO.ySizing, 1);
        GetComponent<SpriteRenderer>().sprite = tileSO.tileImage;
        GetComponent<SpriteRenderer>().sortingLayerName = "Terrain";
    }

    public void SetIndex(float x, float y)
    {
        this.index = new Vector2(x, y);
        gameObject.transform.position = new Vector3(this.Index.x, this.Index.y, 0);
    }

    public void SetIndex(Vector2 v, float sizeX, float sizeY)
    {
        this.index = v;

        float centerXPos = v.x * sizeX - v.y * sizeX;
        float centerYPos = v.y * sizeY + v.x * sizeY;
        
        Vector2 center = new Vector2(centerXPos, centerYPos);

        gameObject.transform.position = new Vector3(center.x, center.y, 0);
    }

    public void AddNeighbour(Tile neighbour)
    {
        this.neighbours.Add(neighbour);
    }

    public void CountHeuristics(Tile start)
    {
        heuristic = Mathf.Abs(Index.x - start.Index.x) * 10 + Mathf.Abs(Index.y - start.Index.y) * 10;
    }

    public void SetHeuristics(int h)
    {
        this.heuristic = h;
    }
    public void SetParent(Tile parent)
    {
        this.g = parent.g + tile.terrainDifficulty;
        this.f = g + heuristic;
        this.parent = parent;
    }
    public void Clear()
    {
        this.g = 0;
        this.parent = null;
    }

    public void SetPickable(IPickable newItem) 
    {
        item = newItem;
    }

    public IPickable GetPickable() 
    {
        if (item == null)
            return null;
        return item;
    }
}
