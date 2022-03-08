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
    }

    private void LateUpdate()
    {
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
