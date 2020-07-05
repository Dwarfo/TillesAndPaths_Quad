using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileChooser : MonoBehaviour
{
    public GameObject content;
    public GameObject tileToChoosePrefab;

    public TileInfoEvent tileChosenEvent = new TileInfoEvent();

    private int numberOfItems = 0;
    public void InvokeTileChosen(SO_Tile tileInfo)
    {
        tileChosenEvent.Invoke(tileInfo);
    }
    public TileToChoose AddTileToChoose(SO_Tile tileInfo) 
    {
        GameObject tileToChoose = Instantiate(tileToChoosePrefab, content.transform);
        RectTransform rt = tileToChoose.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(-160, -80 - 120 * numberOfItems);
        TileToChoose tileToChooseScript = tileToChoose.GetComponent<TileToChoose>();
        tileToChooseScript.SetTileInfo(tileInfo);
        tileToChooseScript.SetTileChooser(this);
        numberOfItems++;

        return tileToChooseScript;
    }
    public void AdjustContent()
    {
        RectTransform contentTransform = content.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, 45 + numberOfItems * 120);
    }
}
