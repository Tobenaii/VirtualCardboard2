using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Modsys/Entity")]
public class ModEntity : ScriptableObject, ISerializationCallbackReceiver
{
    [PropertyOrder(10000)]
    [SerializeField] private ArchetypeAuthoring _authoring;

    public void Instantiate()
    {
        _authoring.Instantiate();
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _authoring.Convert(entity, dstManager, conversionSystem);
    }

    public Entity GetPrefab(EntityManager manager)
    {
        var ecb = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
        return _authoring.GetPrefab(manager);
    }

    [ShowIf("@UnityEngine.Application.isPlaying")] [PropertyOrder(100000)]
    [Button("Test")]
    private void DebugInstantiate()
    {
        Instantiate();
    }

    private void Register()
    {
        if (_authoring != null)
            _authoring.Archetype?.Register(this);
    }

    private void OnValidate()
    {
        Register();
        ValidateComponents();
    }

    public void ValidateComponents()
    {
        if (_authoring.Archetype != null)
            _authoring.ValidateComponents();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        Register();
    }

    public void OnAfterDeserialize()
    {
    }
}
