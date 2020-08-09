using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTileField", menuName = "Tiles/TileField", order = 2)]
public class SO_TileFieldToGen : ScriptableObject
{
    public int sizeX;
    public int sizeY;
    public Biome biome;
    public Difficulty difficulty;

    public TileToPercentage[] tileToPercentage;
}

[System.Serializable]
public struct TileToPercentage
{
    public SO_Tile tileType;
    public int percentage;
}

public enum Biome
{
    Forest,
    Plains
}

public enum Difficulty
{
    Hard,
    Easy,
    Medium
}
