using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class PerformAction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ModEntity _entity;
    [SerializeField] private EntityRef _dealer;
    [SerializeField] private bool _onStart;
    [SerializeField] private bool _onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_onClick)
            Instantiate();
    }

    private void Start()
    {
        if (_onStart)
            Instantiate();
    }

    private void Instantiate()
    {
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var entity = manager.Instantiate(_entity.GetPrefab(manager));
        manager.SetComponentData(entity, new PerformActions() { Dealer = _dealer.Entity });
    }
}
