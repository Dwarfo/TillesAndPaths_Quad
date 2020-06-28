﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorMenu : MonoBehaviour, IMenu
{
    public Button saveButton;
    public Button loadButton;
    public Button mainMenuButton;
    public Button continueButton;
    public Button exitButton;

    public Image panelCanvas;

    private bool menuActive = false;

    void Start()
    {
        if (saveButton != null)
            saveButton.onClick.AddListener(SaveOnClick);
        if (loadButton != null)
            loadButton.onClick.AddListener(LoadOnClick);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GotToMainMenu);
        if (continueButton != null)
            continueButton.onClick.AddListener(Continue);
        if (exitButton != null)
            exitButton.onClick.AddListener(Quit);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleElements();
    }

    public void ToggleElements() 
    {
        panelCanvas.gameObject.SetActive(!panelCanvas.gameObject.activeSelf);
    }

    public void SaveOnClick()
    {
        EditorManager.Instance.SaveMap();
    }

    public void LoadOnClick()
    {
        EditorManager.Instance.LoadMap();
    }

    public void Continue()
    {
        ToggleElements();
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void GotToMainMenu()
    {

    }
}
