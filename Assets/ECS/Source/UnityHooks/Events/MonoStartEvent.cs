using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MonoStartEvent : MonoEvent
{
    [SerializeField] private EntityData _entity;

    private void Start()
    {
        Instantiate(_entity.GetPrefab(World.DefaultGameObjectInjectionWorld.EntityManager));
    }
}
