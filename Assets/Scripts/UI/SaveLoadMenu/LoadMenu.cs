using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    public Button agreeButton;
    public Button backButton;
    public GameSaveChooser saveChooser;
    public SaveDialog loadGameDialog;

    public SaveGameEvent loadSaveEvent = new SaveGameEvent();

    void Start()
    {
        agreeButton.onClick.AddListener(LoadChosenSave);
        backButton.onClick.AddListener(GoBack);
        saveChooser.onSaveEntryChanged.AddListener(InvokeLoad);

        loadGameDialog.yesButton.onClick.AddListener(LoadChosenSave);
    }

    private void InvokeLoad(MapData chosenMd) 
    {
        loadGameDialog.gameObject.SetActive(true);
    }

    private void LoadChosenSave()
    {
        if (saveChooser.SaveEntry != null)
            loadSaveEvent.Invoke(saveChooser.SaveEntry.mapData);

    }

    private void GoBack()
    {
        gameObject.SetActive(false);
    }
}
