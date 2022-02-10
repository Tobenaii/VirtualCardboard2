using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public abstract class ModEntity : ScriptableObject
{
    public abstract void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem);
    public abstract Entity GetPrefab(EntityManager manager);

    public abstract void ValidateComponents();

}

[CreateAssetMenu(menuName = "Modsys/Entity")]
public abstract class ModEntity<T> : ModEntity, ISerializationCallbackReceiver where T : Archetype
{
    [SerializeField] private ArchetypeReference<T> _archetype;
    private Entity _prefab;

    public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        foreach (var component in _archetype.Components)
            component.Component.AuthorComponent(entity, dstManager);
    }

    public override Entity GetPrefab(EntityManager manager)
    {
        if (manager.Exists(_prefab))
            return _prefab;
        var entity = manager.CreateEntity();
        Convert(entity, manager, null);
        manager.AddComponent<Prefab>(entity);
        _prefab = entity;
        return entity;
    }

    public void OnAfterDeserialize()
    {
        
    }

    public override void ValidateComponents()
    {
        if (_archetype.Archetype != null)
            _archetype.ValidateComponents(this);
    }

    public void OnBeforeSerialize()
    {
        _archetype.Archetype.Register(this);
    }
}
