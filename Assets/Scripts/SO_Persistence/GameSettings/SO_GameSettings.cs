using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Setting", menuName = "Settings/CustomSettings", order = 1)]
public class SO_GameSettings : ScriptableObject
{
    public float xSize;
    public float ySize;
    public float pixelOffset;
    public float tileSize;
    
    [Header("Available Tiles")]
    public SO_Tile[] allTileTypes;

    [Header("Isometric tile parameters")]
    public float tileWidth;
    public float tileHeight;
    public GameObject tilePrefab;
}

