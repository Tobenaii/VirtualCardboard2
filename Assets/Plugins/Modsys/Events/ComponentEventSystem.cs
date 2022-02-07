using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public abstract class ComponentEventSystemBase<T> : SystemBase
{
    protected EntityQuery _genericQuery;
    public IComponentEvent<T> componentChangedEvent;

    protected override void OnCreate()
    {
        _genericQuery = GetEntityQuery(ComponentType.ReadOnly<T>());
        _genericQuery.SetChangedVersionFilter(new ComponentType(typeof(T)));
    }
}

public abstract class ComponentEventSystemBase<T, V> : ComponentEventSystemBase<T> where V : struct, IJobChunk
{
    protected abstract V CreateJob();

    protected override void OnUpdate()
    {
        var job = CreateJob();
        var iterator = _genericQuery.GetArchetypeChunkIterator();
        job.RunWithoutJobs(ref iterator);
    }
}

public abstract class ComponentEventSystem<T> : ComponentEventSystemBase<T, ComponentEventSystem<T>.GenericComponentEvent> where T : unmanaged, IComponentData
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

    protected override GenericComponentEvent CreateJob()
    {
        GenericComponentEvent job = new GenericComponentEvent
        {
            genericType = GetComponentTypeHandle<T>(true),
            entityTypeHandle = GetEntityTypeHandle(),
            eventTask = componentChangedEvent
        };
        return job;
    }

}

public abstract class BufferEventSystem<T> : ComponentEventSystemBase<T, BufferEventSystem<T>.GenericComponentEvent> where T : unmanaged, IBufferElementData
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

    protected override GenericComponentEvent CreateJob()
    {
        GenericComponentEvent job = new GenericComponentEvent
        {
            genericType = GetBufferTypeHandle<T>(true),
            entityTypeHandle = GetEntityTypeHandle(),
            eventTask = componentChangedEvent,
        };
        return job;
    }
}
