using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MonoEvent : MonoBehaviour
{
    [SerializeField] private EntityRef _dealer;

    private BeginInitializationEntityCommandBufferSystem _commandBuffer;

    private void Awake()
    {
        _commandBuffer = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    public void Instantiate(Entity action)
    {
        var ecb = _commandBuffer.CreateCommandBuffer();

        var entity = ecb.Instantiate(action);
        ecb.AddComponent(entity, new Dealer() { Entity = _dealer.Entity });
    }
}
