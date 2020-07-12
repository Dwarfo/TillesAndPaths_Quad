using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour
{
    public Button backButton;

    [Header("NewSaveElements")]
    public Button newSaveButton;
    public InputField newSaveInput;

    public SaveDialog newSaveDialog;
    public SaveDialog duplicateDialog;

    [Space]
    public GameSaveChooser saveChooser;

    public MapData ChosenMeta { get { return chosenMapData; } }
    private MapData chosenMapData;
    private SaveGameEvent saveEvent;

    void Start()
    {
        backButton.onClick.AddListener(GoBack);

        newSaveButton.onClick.AddListener(MakeNewSave);
        saveChooser.onSaveEntryChanged.AddListener(RewriteSaveGame);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();
        }
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
        newSaveInput.text = null;
        newSaveInput.ActivateInputField();
    }

    private void RewriteSaveGame(MapData mapData) 
    {
        duplicateDialog.gameObject.SetActive(true);
        this.chosenMapData = mapData;
        duplicateDialog.dialogMessage.text = duplicateDialog.dialogMessage.text.Replace("{!gamesave}", chosenMapData.saveName);
    }

    public void SubscribeToEvent(SaveGameEvent makeSaveEvent) 
    {
        this.saveEvent = makeSaveEvent;
    }

}
