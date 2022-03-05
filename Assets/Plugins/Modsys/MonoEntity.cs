using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MonoEntity : MonoBehaviour, ISerializationCallbackReceiver
{
    [PropertyOrder(10000)]
    [SerializeField] private List<EntityAuthoring> _authoring;


    private void Start()
    {
        var entity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
        Convert(entity, World.DefaultGameObjectInjectionWorld.EntityManager);
    }

    public void Convert(Entity entity, EntityManager dstManager)
    {
        foreach (var authoring in _authoring)
            authoring.Convert(entity, dstManager, null);
    }

    private void OnValidate()
    {
        ValidateComponents();
    }

    public void ValidateComponents()
    {
        if (_authoring == null)
            return;
        foreach (var authoring in _authoring)
        {
            if (authoring != null && authoring.Archetype != null)
                authoring.ValidateComponents();
        }
    }

    public void OnBeforeSerialize()
    {
        ValidateComponents();
    }

    public void OnAfterDeserialize()
    {
    }
}
