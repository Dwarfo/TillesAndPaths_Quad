using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class EditorManager : Singleton_MB<EditorManager>
{
    public TerrainEditor terrainEditor;
    public TileChooser tileChooser;
    public SO_GameSettings gameSettings;

    void Start()
    {
        if (terrainEditor == null)
            terrainEditor = gameObject.AddComponent<TerrainEditor>();

        terrainEditor.SetSettings(gameSettings);
        foreach (SO_Tile tileInfo in gameSettings.allTileTypes)
        {
            TileToChoose ttcScript = tileChooser.AddTileToChoose(tileInfo);
            ttcScript.tileChosenEvent.AddListener(HandleTileChosen);
        }
    }

    public void HandleTileChosen(SO_Tile tileInfo) 
    {
        terrainEditor.SetCurrentTileType(tileInfo.tileType);
    }

    public void SaveMap() 
    {
        MapData md = terrainEditor.GetEditedMapData();

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/map.mp";
        Debug.Log("Path: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        bf.Serialize(stream, md);
        stream.Close();
    }

    public void LoadMap() 
    {
        string path = Application.persistentDataPath + "/map.mp";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData mapData = bf.Deserialize(stream) as MapData;
            Debug.Log("Mapdata size: " + mapData.tiles.Count);
            foreach(TileEntry entry in mapData.tiles) 
            {
                Debug.Log("Tile: " + entry.xPosition + ":" + entry.yPosition + "  type: " + entry.tileType);
            }
            
            //TODO make generator that populates tilefield
            stream.Close();
            //TODO generate relations and graph if not in map editor
        }
        else
        {
            Debug.Log("No file found");
        }
    }
}
