using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class SceneEntityMono : MonoBehaviour, ISerializationCallbackReceiver
{
    [PropertyOrder(10000)]
    [SerializeField] private List<EntityAuthoring> _authoring;

    private Entity _entity;
    private bool _initialized;

    private void Start()
    {
        var mng = World.DefaultGameObjectInjectionWorld.EntityManager;
        var entity = mng.CreateEntity();
        mng.SetName(entity, gameObject.name);
        Convert(entity, mng);
        _entity = entity;
        _initialized = true;
    }

    private void UpdateEntity(Entity entity, EntityManager dstManager)
    {
        foreach (var authoring in _authoring)
            authoring.Update(dstManager);
    }

    private void Convert(Entity entity, EntityManager dstManager)
    {
        foreach (var authoring in _authoring)
            authoring.Convert(entity, dstManager);
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
            if (authoring != null && authoring.Template != null)
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
