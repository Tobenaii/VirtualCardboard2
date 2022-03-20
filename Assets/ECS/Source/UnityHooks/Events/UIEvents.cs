using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsHovering { get; private set; }
    public bool HasClicked { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        HasClicked = true;
        StartCoroutine(Clicked());
    }

    //TODO: This is extremely trash
    private IEnumerator Clicked()
    {
        yield return null;
        HasClicked = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovering = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovering = true;
    }
}
