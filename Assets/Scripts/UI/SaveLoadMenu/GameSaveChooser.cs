using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveChooser : MonoBehaviour, ISaveAssigner
{
    public GameObject content;
    public GameObject saveToChoosePrefab;
    public SaveGameEntry SaveEntry { get { return saveGameEntry; } }

    public int numberOfItems = 0;
    [SerializeField]
    private SaveGameEntry saveGameEntry;

    public SaveGameEvent onSaveEntryChanged = new SaveGameEvent();

    public void ResetChosenSave() 
    {
        saveGameEntry = null;
    }

    public void setSaveData(SaveGameEntry sge) 
    {
        saveGameEntry = sge;
        onSaveEntryChanged.Invoke(saveGameEntry.mapData);
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

    public void ClearEntries() 
    {
        SaveGameEntry[] entryList = content.GetComponentsInChildren<SaveGameEntry>();
        foreach (SaveGameEntry saveEntry in entryList) 
        {
            Destroy(saveEntry.gameObject);
        }
    }
}
