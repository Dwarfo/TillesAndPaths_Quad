using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    public Button loadGameButton;
    public Button backButton;
    public GameSaveChooser saveChooser;

    public SaveGameEvent loadSaveEvent = new SaveGameEvent();

    void Start()
    {
        loadGameButton.onClick.AddListener(LoadChosenSave);
        backButton.onClick.AddListener(GoBack);
    }

    private void LoadChosenSave()
    {
        if (saveChooser.SaveEntry != null)
            loadSaveEvent.Invoke(saveChooser.SaveEntry.mapData);
    }

    private void GoBack()
    {

    }
}
