using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDialog : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public Text dialogMessage;//{!gamesave}

    void Start()
    {
        noButton.onClick.AddListener(GoBack);
        yesButton.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
    }
}
