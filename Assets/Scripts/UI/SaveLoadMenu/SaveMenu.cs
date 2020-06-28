using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour
{
    public Button saveGameButton;
    public Button backButton;
    public GameSaveChooser saveChooser;

    public SaveGameEvent loadSaveEvent = new SaveGameEvent();

    void Start()
    {
        saveGameButton.onClick.AddListener(LoadChosenSave);
        backButton.onClick.AddListener(GoBack);
    }

    private void LoadChosenSave()
    {
        //create saveGame
    }

    private void GoBack()
    {

    }
}
