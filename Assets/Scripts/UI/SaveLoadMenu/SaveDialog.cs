using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDialog : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;

    void Start()
    {
        noButton.onClick.AddListener(DisagreedOnNewSave);
    }

    private void DisagreedOnNewSave()
    {
        gameObject.SetActive(false);
    }
}
