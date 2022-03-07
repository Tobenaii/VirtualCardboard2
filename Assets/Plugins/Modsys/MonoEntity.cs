using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class MonoEntity : MonoBehaviour, ISerializationCallbackReceiver
{
    [PropertyOrder(10000)]
    [SerializeField] private List<EntityAuthoring> _authoring;

    private Entity _entity;
    private bool _initialized;

    private void Start()
    {
        var entity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
        Convert(entity, World.DefaultGameObjectInjectionWorld.EntityManager);
        _entity = entity;
        _initialized = true;
    }

    private void UpdateEntity(Entity entity, EntityManager dstManager)
    {
        foreach (var authoring in _authoring)
            authoring.Update(entity, dstManager, null);
    }

    private void Convert(Entity entity, EntityManager dstManager)
    {
        foreach (var authoring in _authoring)
            authoring.Convert(entity, dstManager, null);
    }

    private void OnValidate()
    {
        ValidateComponents();
        if (Application.isPlaying && _initialized)
            UpdateEntity(_entity, World.DefaultGameObjectInjectionWorld.EntityManager);
    }

    public void ValidateComponents()
    {
        if (_authoring == null)
            return;
        
        foreach (var authoring in _authoring.ToList())
        {
            if (authoring == null)
                _authoring.Remove(authoring);
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
