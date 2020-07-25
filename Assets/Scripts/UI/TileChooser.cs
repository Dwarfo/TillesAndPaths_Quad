using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileChooser : MonoBehaviour
{
    public GameObject content;
    public GameObject tileToChoosePrefab;

    public TileInfoEvent tileChosenEvent = new TileInfoEvent();
    public BoolEvent hoverOverTileChooser = new BoolEvent();

    private int numberOfItems = 0;
    [SerializeField]
    private SelectableUIScript uiScript;

    private void Start()
    {
        uiScript.Initiate();
        uiScript.onEnter.AddListener(FireOnEnter);
        uiScript.onExit.AddListener(FireOnExit);
    }

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

    private void FireOnEnter()
    {
        EditorManager.Instance.currentState.HoverOver(EditorManager.Instance);
    }

    private void FireOnExit()
    {
        EditorManager.Instance.currentState.ExitFromTileChooser(EditorManager.Instance);
    }

    public bool IsHovering() 
    {
        return uiScript.IsHovering();
    }
}
