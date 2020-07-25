using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Setting", menuName = "Settings/CustomSettings", order = 1)]
public class SO_GameSettings : ScriptableObject
{
    [Header("Map Parameters")]
    public int xSize;
    public int ySize;
    
    [Header("Available Tiles")]
    public SO_Tile[] allTileTypes;

    [Header("Isometric tile parameters")]
    public float tileWidth;
    public float tileHeight;
    public GameObject tilePrefab;
}

