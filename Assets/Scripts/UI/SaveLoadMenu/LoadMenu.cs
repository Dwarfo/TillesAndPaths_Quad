using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    public Button backButton;

    public GameSaveChooser saveChooser;
    public SaveDialog loadGameDialog;

    void Start()
    {
        backButton.onClick.AddListener(GoBack);
        saveChooser.onSaveEntryChanged.AddListener(InvokeLoad);

        loadGameDialog.yesButton.onClick.AddListener(LoadChosenSave);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();
        }
    }

    private void InvokeLoad(MapData chosenMd) 
    {
        loadGameDialog.gameObject.SetActive(true);
        loadGameDialog.dialogMessage.text = loadGameDialog.dialogMessage.text.Replace("{!gamesave}", chosenMd.saveName);
    }

    private void LoadChosenSave()
    {
        loadGameDialog.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
    }
}
