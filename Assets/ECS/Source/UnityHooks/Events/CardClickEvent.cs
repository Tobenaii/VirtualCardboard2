using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClickEvent : MonoEvent, IPointerClickHandler
{
    private Entity _card;

    public void SetCard(Entity card)
    {
        _card = card;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(_card);
    }
}
