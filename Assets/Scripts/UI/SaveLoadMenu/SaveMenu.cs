using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour
{
    public Button saveGameButton;
    public Button backButton;

    [Header("NewSaveElements")]
    public Button newSaveButton;
    public InputField newSaveInput;

    public SaveDialog newSaveDialog;
    public SaveDialog duplicateDialog;

    [Space]
    public GameSaveChooser saveChooser;

    private MapData chosenMapData;

    public SaveGameEvent makeSaveEvent = new SaveGameEvent();

    void Start()
    {
        backButton.onClick.AddListener(GoBack);

        newSaveButton.onClick.AddListener(MakeNewSave);
        saveChooser.onSaveEntryChanged.AddListener(RewriteSaveGame);

        duplicateDialog.yesButton.onClick.AddListener(AgreedToRewriteSave);
        newSaveDialog.yesButton.onClick.AddListener(AgreedOnNewSave);
    }

    private void GoBack()
    {
        this.chosenMapData = null;
        gameObject.SetActive(false);
    }

    private void MakeNewSave()
    {
        Debug.Log("New Save");
        newSaveDialog.gameObject.SetActive(true);
    }

    private void RewriteSaveGame(MapData mapData) 
    {
        duplicateDialog.gameObject.SetActive(true);
        this.chosenMapData = mapData;

        //GameManager save with old name
    }

    private void AgreedOnNewSave()
    {
        string saveName = newSaveInput.text;
        
        //GameManager save new
    }

    private void AgreedToRewriteSave()
    {
        makeSaveEvent.Invoke(saveChooser.SaveEntry.mapData);
    }

}
