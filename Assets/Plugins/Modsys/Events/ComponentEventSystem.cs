using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public abstract class ComponentEventSystem<T> : SystemBase where T : unmanaged, IComponentData
{
    public struct GenericComponentEvent : IJobChunk
    {
        [ReadOnly] public ComponentTypeHandle<T> genericType;
        [ReadOnly] public EntityTypeHandle entityTypeHandle;
        [ReadOnly] public IComponentEvent<T> eventTask;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<T> generics = chunk.GetNativeArray(genericType);
            NativeArray<Entity> entities = chunk.GetNativeArray(entityTypeHandle);
            for (int i = 0; i < generics.Length; i++)
            {
                var componentEvent = eventTask;
                componentEvent.OnComponentChanged(generics[i], entities[i]);
            }
        }
    }

    private EntityQuery _genericQuery;
    public IComponentEvent<T> componentChangedEvent;

    protected override void OnCreate()
    {
        _genericQuery = GetEntityQuery(ComponentType.ReadOnly<T>());
        _genericQuery.SetChangedVersionFilter(new ComponentType(typeof(T)));
    }

    protected override void OnUpdate()
    {
        GenericComponentEvent job = new GenericComponentEvent
        {
            genericType = GetComponentTypeHandle<T>(true),
            entityTypeHandle = GetEntityTypeHandle(),
            eventTask = componentChangedEvent
        };
        var iterator = _genericQuery.GetArchetypeChunkIterator();
        job.RunWithoutJobs(ref iterator);
    }
}

public abstract class BufferEventSystem<T> : SystemBase where T : unmanaged, IBufferElementData
{
    public struct GenericComponentEvent : IJobChunk
    {
        [ReadOnly] public BufferTypeHandle<T> genericType;
        [ReadOnly] public EntityTypeHandle entityTypeHandle;
        [ReadOnly] public IComponentEvent<T> eventTask;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            BufferAccessor<T> generics = chunk.GetBufferAccessor(genericType);
            NativeArray<Entity> entities = chunk.GetNativeArray(entityTypeHandle);
            for (int i = 0; i < generics.Length; i++)
            {
                for (int x = 0; x < generics[i].Length; x++)
                    eventTask.OnComponentChanged(generics[i][x], entities[i]);
            }
        }
    }

    private EntityQuery _genericQuery;
    public IComponentEvent<T> componentChangedEvent;

    protected override void OnCreate()
    {
        _genericQuery = GetEntityQuery(ComponentType.ReadOnly<T>());
        _genericQuery.SetChangedVersionFilter(new ComponentType(typeof(T)));
    }

    protected override void OnUpdate()
    {
        GenericComponentEvent job = new GenericComponentEvent
        {
            genericType = GetBufferTypeHandle<T>(true),
            entityTypeHandle = GetEntityTypeHandle(),
            eventTask = componentChangedEvent,
        };
        var iterator = _genericQuery.GetArchetypeChunkIterator();
        job.RunWithoutJobs(ref iterator);
    }
}
