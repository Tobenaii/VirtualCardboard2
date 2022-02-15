using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DeckCollectionSystem : CollectionContainerSystem<Deck, DeckCard> { }


public abstract class CollectionContainerSystem<Container, Collection> : SystemBase where Container : struct, IPrefabCollectionContainer<Collection> where Collection : struct, IPrefabCollection
{
    public struct GenericContainerJob : IJobChunk
    {
        public ComponentTypeHandle<Container> containerType;
        [ReadOnly] public BufferTypeHandle<Collection> collectionType;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<Container> containers = chunk.GetNativeArray(containerType);
            BufferAccessor<Collection> collections = chunk.GetBufferAccessor(collectionType);
            for (int i = 0; i < containers.Length; i++)
            {
                var container = containers[i];
                container.CurrentCount = collections[i].Length;
                containers[i] = container;
            }
        }
    }

    protected EntityQuery _genericQuery;
    protected override void OnCreate()
    {
        _genericQuery = GetEntityQuery(ComponentType.ReadWrite<Container>() ,ComponentType.ReadOnly<Collection>());
        _genericQuery.SetChangedVersionFilter(new ComponentType(typeof(Collection)));
    }

    private GenericContainerJob CreateJob()
    {
        GenericContainerJob job = new GenericContainerJob
        {
            containerType = GetComponentTypeHandle<Container>(false),
            collectionType = GetBufferTypeHandle<Collection>(true),
        };
        return job;
    }

    protected override void OnUpdate()
    {
        var job = CreateJob();
        job.Run(_genericQuery);
    }
}
