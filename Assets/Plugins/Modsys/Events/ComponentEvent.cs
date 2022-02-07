using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IComponentListener<T> where T : unmanaged
{
    public void OnComponentChanged(T newValue);
}

public interface IComponentEvent<T> where T : unmanaged
{
    public void OnComponentChanged(T value, Entity entity);
}

public abstract class ComponentEvent : ScriptableObject
{
    public abstract void Init();
    public abstract void Disable();
}

public abstract class ComponentEvent<T, V> : ComponentEvent, IComponentEvent<T> where T : unmanaged, IComponentData where V : ComponentEventSystem<T>
{
    private Dictionary<Entity, List<IComponentListener<T>>> _listenerMap = new Dictionary<Entity, List<IComponentListener<T>>>();

    public override void Init()
    {
        ValidateMap();
        var world = World.DefaultGameObjectInjectionWorld;
        var system = world.GetOrCreateSystem<V>();
        var sim = world.GetOrCreateSystem<PresentationSystemGroup>();
        sim.AddSystemToUpdateList(system);
        system.componentChangedEvent = this;
    }

    public override void Disable()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
            return;
        var system = world.GetExistingSystem<V>();
        var sim = world.GetExistingSystem<PresentationSystemGroup>();
        sim.RemoveSystemFromUpdateList(system);
    }

    public void ValidateMap()
    {
        var remove = new List<Entity>();
        foreach (var key in _listenerMap.Keys)
        {
            var listeners = _listenerMap[key];
            listeners.RemoveAll(x => x.Equals(null));
            if (listeners.Count == 0)
                remove.Add(key);
        }
        foreach (var entity in remove)
            _listenerMap.Remove(entity);
    }

    private void Clear()
    {
        _listenerMap.Clear();
    }

    public void Register(Entity entity, IComponentListener<T> listener)
    {
        List<IComponentListener<T>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
            listeners.Add(listener);
        else
        {
            listeners = new List<IComponentListener<T>>();
            listeners.Add(listener);
            _listenerMap.Add(entity, listeners);
        }
    }

    public void Unregister(Entity entity)
    {
        _listenerMap.Remove(entity);
    }

    public void OnComponentChanged(T newValue, Entity entity)
    {
        List<IComponentListener<T>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnComponentChanged(newValue);
            }
        }
    }
}

public abstract class BufferComponentEvent<T, V> : ComponentEvent, IComponentEvent<DynamicBuffer<T>> where T : unmanaged, IBufferElementData where V : BufferComponentEventSystem<T>
{
    private Dictionary<Entity, List<IComponentListener<DynamicBuffer<T>>>> _listenerMap = new Dictionary<Entity, List<IComponentListener<DynamicBuffer<T>>>>();

    public override void Init()
    {
        ValidateMap();
        var world = World.DefaultGameObjectInjectionWorld;
        var system = world.GetOrCreateSystem<V>();
        var sim = world.GetOrCreateSystem<PresentationSystemGroup>();
        sim.AddSystemToUpdateList(system);
        system.componentChangedEvent = this;
    }

    public override void Disable()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
            return;
        var system = world.GetExistingSystem<V>();
        var sim = world.GetExistingSystem<PresentationSystemGroup>();
        sim.RemoveSystemFromUpdateList(system);
    }

    private void Clear()
    {
        _listenerMap.Clear();
    }
    public void ValidateMap()
    {
        var remove = new List<Entity>();
        foreach (var key in _listenerMap.Keys)
        {
            var listeners = _listenerMap[key];
            listeners.RemoveAll(x => x.Equals(null));
            if (listeners.Count == 0)
                remove.Add(key);
        }
        foreach (var entity in remove)
            _listenerMap.Remove(entity);
    }
    public void Register(Entity entity, IComponentListener<DynamicBuffer<T>> listener)
    {
        List<IComponentListener<DynamicBuffer<T>>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
            listeners.Add(listener);
        else
        {
            listeners = new List<IComponentListener<DynamicBuffer<T>>>();
            listeners.Add(listener);
            _listenerMap.Add(entity, listeners);
        }
    }

    public void Unregister(Entity entity)
    {
        _listenerMap.Remove(entity);
    }

    public void OnComponentChanged(DynamicBuffer<T> newValue, Entity entity)
    {
        List<IComponentListener<DynamicBuffer<T>>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnComponentChanged(newValue);
            }
        }
    }
}
