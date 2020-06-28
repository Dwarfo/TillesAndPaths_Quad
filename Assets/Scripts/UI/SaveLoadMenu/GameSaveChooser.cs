using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveChooser : MonoBehaviour, ISaveAssigner
{
    public GameObject content;
    public GameObject saveToChoosePrefab;
    public SaveGameEntry SaveEntry { get { return saveGameEntry; } }

    private int numberOfItems = 0;
    [SerializeField]
    private SaveGameEntry saveGameEntry;

    public void ResetChosenSave() 
    {
        saveGameEntry = null;
        //make save look unchosen,
    }
    public void setSaveData(SaveGameEntry sge) 
    {
        saveGameEntry = sge;
        //make save look chosen, and make currently chosen entry not chosen
    }

    public void AdjustContent() 
    {
        RectTransform contentTransform = content.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, numberOfItems * 160);
    }

    public SaveGameEntry AssignSaveData(MapData md)
    {
        GameObject saveToChoose = Instantiate(saveToChoosePrefab, content.transform);
        RectTransform rt = saveToChoose.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(0, -160 * numberOfItems);
        SaveGameEntry saveEntryScript = saveToChoose.GetComponent<SaveGameEntry>();
        saveEntryScript.SetSaveInfo(md);
        saveEntryScript.SetSaveChooser(this);
        numberOfItems++;

        return saveEntryScript;
    }
}
