﻿using System.Collections;
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
    public CameraControls cameraControls;
    public bool InMenu { get { return inMenu; } }

    [Header("UI")]
    public EditorMenu editorMenu;

    public IMenuState currentState;
    public IMenuState editingState;
    public IMenuState hoverState;
    public IMenuState inMenuState;

    private bool inMenu;

    void Start()
    {
        if (terrainEditor == null)
            terrainEditor = gameObject.AddComponent<TerrainEditor>();

        if (editorMenu != null)
            editorMenu.menuToggled.AddListener(ShutEditor);

        terrainEditor.SetSettings(gameSettings);

        editorMenu.loadSaveEvent.AddListener(LoadMap);
        editorMenu.makeSaveEvent.AddListener(SaveMap);

        editingState = new EditingState();
        hoverState = new HoverState();
        inMenuState = new InMenuState();

        currentState = editingState;

        FillTileChooser();
        cameraControls.SetSize(gameSettings);
    }

    private void FillTileChooser() 
    {
        //Add event handler for tile newTiletype chosen, add all tiles to be available for choosing
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

    public void SaveMap(MapData saveNameMd) 
    {
        MapData md = terrainEditor.GetEditedMapData();
        md.saveName = saveNameMd.saveName;

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + md.saveName + ".mp";
        Debug.Log("Path: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        bf.Serialize(stream, md);
        stream.Close();

        //return md;
    }

    public void LoadMap(MapData md) 
    {
        string path = Application.persistentDataPath + "/" + md.saveName + ".mp";
        
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData mapData = bf.Deserialize(stream) as MapData;
            Debug.Log("Mapdata size: " + mapData.tiles.Count);
            foreach(TileEntry entry in mapData.tiles) 
            {
                Debug.Log("Tile: " + entry.xPosition + ":" + entry.yPosition + "  type: " + entry.tileType);
                Vector2 position = new Vector2(entry.xPosition, entry.yPosition);
                terrainEditor.PutTileInField(position, entry.tileType);
            }
            
            stream.Close();
        }
        else
        {
            Debug.Log("No file found");
        }
    }

    public void FillGameSaves(GameSaveChooser saveChooser, int startingIndex)
    {
        saveChooser.ClearEntries();

        List<MapData> mdList = new List<MapData>();
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath;
        saveChooser.numberOfItems = startingIndex;

        foreach (string file in Directory.EnumerateFiles(path, "*.mp"))
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            MapData data = bf.Deserialize(fs) as MapData;
            saveChooser.AssignSaveData(data);
            mdList.Add(data);
            fs.Close();
        }

        saveChooser.AdjustContent();
    }

    private void ShutEditor(bool menuToggled) 
    {
        //Debug.Log("Invoked: " + menuToggled);
        if (!menuToggled)
            if (tileChooser.IsHovering())
                currentState.MenuBlocked(this);
            else
                currentState.MenuExited(this);
        else
            currentState.MenuEnetered(this);

        UIManager.Instance.lineDrawer.ClearLine();
    }

    public void GoToHoveringState() 
    {
        currentState = hoverState;
        UIManager.Instance.lineDrawer.ClearLine();
    }

    public void GoToInMenuState() 
    {
        currentState = inMenuState;
    }

    public void GoToEditingState() 
    {
        currentState = editingState;
    }
}
