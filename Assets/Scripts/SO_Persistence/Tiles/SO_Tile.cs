using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tiles/NewTile", order = 1)]
public class SO_Tile : ScriptableObject
{
    public string tileName;
    [Tooltip("Weight for pathfinding algorithm")]
    public int terrainDifficulty;
    [Tooltip("If true, player cannot stand on this tile, and it will be omitted in pathfinding")]
    public bool impassible;
    public TileTypes tileType;
    public Sprite tileImage;

    [Header("Sizing for instantiated tiles")]
    public float xSizing;
    public float ySizing;
}

public enum TileTypes
{
    Empty = 0,
    Mountain = 1,
    Grass = 2,
    RockGround = 3
}