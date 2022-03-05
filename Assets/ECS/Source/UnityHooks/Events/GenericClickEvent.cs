using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class GenericClickEvent : MonoEvent, IPointerClickHandler
{
    [SerializeField] private ModEntity _action;

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(_action.GetPrefab(World.DefaultGameObjectInjectionWorld.EntityManager, _action.name));
    }
}
