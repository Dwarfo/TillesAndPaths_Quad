using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameEntry : MonoBehaviour
{
    public Button gameSaveButton;
    public Text saveDescr;
    public MapData mapData;

    private GameSaveChooser seChooser;


    void Start()
    {
        if (gameSaveButton == null)
        {
            gameSaveButton = gameObject.GetComponentInChildren<Button>();
            saveDescr = gameObject.GetComponentInChildren<Text>();
        }
        gameSaveButton.onClick.AddListener(ChooseSave);
    }

    public void ChooseSave()
    {
        Debug.Log("ClickedSavegame");
        seChooser.setSaveData(this);
    }

    public void SetSaveInfo(MapData md)
    {
        mapData = md;
        saveDescr.text = md.saveName + "\n" + "Tiles: " + md.tiles.Count;
    }

    public void SetSaveChooser(GameSaveChooser seChooser)
    {
        this.seChooser = seChooser;
    }
}
