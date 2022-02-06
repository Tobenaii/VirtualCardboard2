using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ComponentEvent : SerializedMonoBehaviour
{
    [SerializeField] private ArchetypeReference _components;

    public void SpawnEntityComponents()
    {
        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        var entity = em.CreateEntity();
        foreach (var component in _components.Components)
        {
            component.Component.AuthorComponent(entity, em);
        }
    }
}
