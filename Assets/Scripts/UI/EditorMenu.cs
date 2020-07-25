using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EditorMenu : MonoBehaviour
{
    [Header("MainMenuButtons")]
    public Button saveButton;
    public Button loadButton;
    public Button mainMenuButton;
    public Button continueButton;
    public Button exitButton;

    [Header("Save/Load menus")]
    public LoadMenu loadMenu;
    public SaveMenu saveMenu;
    public Button agreeSaveButton;
    public Button agreeLoadButton;

    public Image panelCanvas;

    public BoolEvent menuToggled = new BoolEvent();

    private bool menuActive = false;

    public SaveGameEvent loadSaveEvent = new SaveGameEvent();
    public SaveGameEvent makeSaveEvent = new SaveGameEvent();

    void Start()
    {
        if (saveButton != null)
            saveButton.onClick.AddListener(GoToSaveMenu);
        if (loadButton != null)
            loadButton.onClick.AddListener(GoToLoadMenu);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (continueButton != null)
            continueButton.onClick.AddListener(ReturnToEditing);
        if (exitButton != null)
            exitButton.onClick.AddListener(Quit);

        saveMenu.backButton.onClick.AddListener(GoToMenu);
        saveMenu.SubscribeToEvent(makeSaveEvent);

        saveMenu.newSaveDialog.yesButton.onClick.AddListener(AgreedOnNewSave);
        saveMenu.duplicateDialog.yesButton.onClick.AddListener(AgreedToRewriteSave);

        loadMenu.loadGameDialog.yesButton.onClick.AddListener(ReturnToEditing);
        loadMenu.loadGameDialog.yesButton.onClick.AddListener(LoadSavegame);
        
        loadMenu.backButton.onClick.AddListener(GoToMenu);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (loadMenu.isActiveAndEnabled || saveMenu.isActiveAndEnabled)
            {
                return;
            }

            if (menuActive)
                ReturnToEditing();
            else
                GoToMenu();

            menuToggled.Invoke(menuActive);
        }
    }

    private void ReturnToEditing()
    {
        panelCanvas.gameObject.SetActive(false);
        menuToggled.Invoke(false);
        menuActive = false;
    }

    private void GoToMenu()
    {
        panelCanvas.gameObject.SetActive(true);
        menuToggled.Invoke(true);
        menuActive = true;
    }

    private void GoToLoadMenu()
    {
        panelCanvas.gameObject.SetActive(false);
        loadMenu.gameObject.SetActive(true);

        EditorManager.Instance.FillGameSaves(loadMenu.saveChooser, 0);
    }

    private void GoToSaveMenu()
    {
        panelCanvas.gameObject.SetActive(false);
        saveMenu.gameObject.SetActive(true);

        EditorManager.Instance.FillGameSaves(saveMenu.saveChooser, 1);
    }
    private void GoToMainMenu()
    {
        //mainmenu
    }

    private void Quit() 
    {
        Application.Quit();
    }

    private void LoadSavegame() 
    {
        if (loadMenu.saveChooser.SaveEntry != null)
            loadSaveEvent.Invoke(loadMenu.saveChooser.SaveEntry.mapData);
    }

    private void AgreedOnNewSave()
    {
        string saveName = saveMenu.newSaveInput.text;
        MapData mapData = new MapData();
        mapData.saveName = saveName;

        makeSaveEvent.Invoke(mapData);

        EditorManager.Instance.FillGameSaves(saveMenu.saveChooser, 1);
    }

    private void AgreedToRewriteSave()
    {
        MapData mapData = saveMenu.ChosenMeta;

        makeSaveEvent.Invoke(mapData);

        EditorManager.Instance.FillGameSaves(saveMenu.saveChooser, 1);
    }
}