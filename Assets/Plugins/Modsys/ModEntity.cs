using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Modsys/Entity")]
public class ModEntity : ScriptableObject, ISerializationCallbackReceiver
{
    [ShowIf("@_archetype.Archetype != null")] [PropertyOrder(-1000)]
    [ShowInInspector] public Archetype Archetype => (Archetype)_archetype.Archetype;
    [PropertyOrder(10000)]
    [SerializeField] private ArchetypeReference _archetype;
    private Entity _prefab;

    public void Instantiate()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.Instantiate(GetPrefab(entityManager));
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        foreach (var component in _archetype.Components)
            component.Component.AuthorComponent(entity, dstManager);
    }

    public Entity GetPrefab(EntityManager manager)
    {
        //TODO: Work out how to have _hasPrefab not survive sessions
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
        Instantiate();
    }

    public void OnAfterDeserialize()
    {
        
    }

    private void OnValidate()
    {
        ValidateComponents();
    }

    public void ValidateComponents()
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
