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
    [ShowIf("@_archetype.Archetype != null")] [PropertyOrder(-1000)]
    [ShowInInspector] public T Archetype => (T)_archetype.Archetype;
    [PropertyOrder(10000)]
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

    [ShowIf("@UnityEngine.Application.isPlaying")] [PropertyOrder(100000)]
    [Button("Test")]
    private void DebugInstantiate()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.Instantiate(GetPrefab(entityManager));
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
        if (_archetype != null)
            _archetype.Archetype?.Register(this);
    }
}
