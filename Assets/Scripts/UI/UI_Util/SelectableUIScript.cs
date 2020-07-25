using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableUIScript : Selectable
{
    public EmptyEvent onEnter;
    public EmptyEvent onExit;

    public void Initiate()
    {
        onEnter = new EmptyEvent();
        onExit = new EmptyEvent();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onEnter.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onExit.Invoke();
    }

    public bool IsHovering() 
    {
        return IsHighlighted();
    }
}
