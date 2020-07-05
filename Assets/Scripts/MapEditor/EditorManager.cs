using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class EditorManager : Singleton_MB<EditorManager>
{
    public TerrainEditor terrainEditor;
    public TileChooser tileChooser;
    public GameSaveChooser saveChooserSaves;
    public GameSaveChooser saveChooserLoader;
    public SO_GameSettings gameSettings;
    public bool InMenu { get { return inMenu; } }

    [Header("UI")]
    public EditorMenu editorMenu;

    private bool inMenu;

    void Start()
    {
        if (terrainEditor == null)
            terrainEditor = gameObject.AddComponent<TerrainEditor>();

        if (editorMenu != null)
            editorMenu.menuToggled.AddListener(ShutEditor);

        terrainEditor.SetSettings(gameSettings);

        
        FillTileChooser();
        FillGameSaves();
    }

    private void FillTileChooser() 
    {
        //Add event handler for tile newTIletype chosen, add all tiles to be available for choosing
        tileChooser.tileChosenEvent.AddListener(HandleTileChosen);

        foreach (SO_Tile tileInfo in gameSettings.allTileTypes)
        {
            TileToChoose ttcScript = tileChooser.AddTileToChoose(tileInfo);
        }

        tileChooser.AdjustContent();
    }

    public void HandleTileChosen(SO_Tile tileInfo) 
    {
        terrainEditor.SetCurrentTileType(tileInfo.tileType);
    }

    public MapData SaveMap(string saveName) 
    {
        MapData md = terrainEditor.GetEditedMapData();

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + saveName + ".mp";
        Debug.Log("Path: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        bf.Serialize(stream, md);
        stream.Close();

        return md;
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

    private void FillGameSaves()
    {
        List<MapData> mdList = new List<MapData>();
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath;
        saveChooserSaves.numberOfItems = 1;

        foreach (string file in Directory.EnumerateFiles(path, "*.mp"))
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            MapData data = bf.Deserialize(fs) as MapData;
            saveChooserLoader.AssignSaveData(data);
            saveChooserSaves.AssignSaveData(data);
            mdList.Add(data);
            fs.Close();
        }

        saveChooserSaves.AdjustContent();
        saveChooserLoader.AdjustContent();
    }

    private void ShutEditor(bool menuToggled) 
    {
        Debug.Log("Invoked: " + menuToggled);
        inMenu = menuToggled;
        UIManager.Instance.lineDrawer.ClearLine();
    }
}
