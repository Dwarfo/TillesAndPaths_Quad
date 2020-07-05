using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorMenu : MonoBehaviour, IMenu
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

    public Image panelCanvas;

    public BoolEvent menuToggled = new BoolEvent();

    void Start()
    {
        if (saveButton != null)
            saveButton.onClick.AddListener(OpenSaveMenu);
        if (loadButton != null)
            loadButton.onClick.AddListener(LoadOnClick);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (continueButton != null)
            continueButton.onClick.AddListener(Continue);
        if (exitButton != null)
            exitButton.onClick.AddListener(Quit);

        saveMenu.backButton.onClick.AddListener(Back);
        loadMenu.backButton.onClick.AddListener(Back);
    }

    void Update()
    {
        if (loadMenu.gameObject.activeSelf || saveMenu.gameObject.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleElements();
            menuToggled.Invoke(panelCanvas.gameObject.activeSelf);
        }
    }

    public void ToggleElements() 
    {
        panelCanvas.gameObject.SetActive(!panelCanvas.gameObject.activeSelf);
    }

    public void OpenSaveMenu()
    {
        ToggleElements();
        saveMenu.gameObject.SetActive(true);
    }

    public void LoadOnClick()
    {
        ToggleElements();
        loadMenu.gameObject.SetActive(true);
    }

    public void Continue()
    {
        ToggleElements();
        menuToggled.Invoke(panelCanvas.gameObject.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        menuToggled.Invoke(false);
    }

    private void Back() 
    {
        ToggleElements();
    }
}
