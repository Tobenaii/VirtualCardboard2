using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class EntityDataMono : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private EntityRef _dealer;
    [PropertyOrder(10000)]
    [SerializeField] private EntityDataAuthoring _authoring;

    private Entity _entity;

    private void Start()
    {
        var mng = World.DefaultGameObjectInjectionWorld.EntityManager;
        var entity = mng.CreateEntity();
        mng.SetName(entity, gameObject.name);
        Convert(entity, mng);
        _entity = entity;
    }

    private void Convert(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new Dealer() { Entity = _dealer.Entity });
        _authoring.Convert(entity, dstManager);
    }

    private void OnValidate()
    {
        if (Application.isPlaying && _entity != default)
            _authoring.Update(_entity, World.DefaultGameObjectInjectionWorld.EntityManager);
    }

    public void OnBeforeSerialize()
    {
        if (_authoring != null)
            _authoring.ValidateComponents();
    }

    public void OnAfterDeserialize()
    {
    }
}
