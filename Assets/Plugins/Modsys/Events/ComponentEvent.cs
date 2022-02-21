using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IBufferFlag
{
    public enum Flag { Added, Removed, None };
    public Flag BufferFlag { get; set; }
}

public interface IComponentChangedListener<T>
{
    public void OnComponentChanged(T value);
}

public interface IComponentAddedListener<T>
{
    public void OnComponentAdded(T value);
}

public interface IComponentChangedEvent<T>
{
    public void OnComponentChanged(T value, Entity entity);
    public void OnComponentAdded(T value, Entity entity);
}

public abstract class ComponentEvent : ScriptableObject 
{
    public abstract void Init();
    public abstract void Disable();
}

public abstract class ComponentEvent<U> : ComponentEvent
{
    protected Dictionary<Entity, List<IComponentChangedListener<U>>> _listenerChangedMap = new Dictionary<Entity, List<IComponentChangedListener<U>>>();
    protected Dictionary<Entity, List<IComponentAddedListener<U>>> _listenerAddedMap = new Dictionary<Entity, List<IComponentAddedListener<U>>>();

    protected List<IComponentChangedListener<U>> _listeners = new List<IComponentChangedListener<U>>();

    public void RegisterChanged(Entity entity, IComponentChangedListener<U> listener)
    {
        Register(entity, listener, _listenerChangedMap);
    }

    public void RegisterAdded(Entity entity, IComponentAddedListener<U> listener)
    {
        Register(entity, listener, _listenerAddedMap);
    }

    private void Register<V>(Entity entity, V listener, Dictionary<Entity, List<V>> map)
    {
        List<V> listeners;
        if (map.TryGetValue(entity, out listeners))
            listeners.Add(listener);
        else
        {
            listeners = new List<V>();
            listeners.Add(listener);
            map.Add(entity, listeners);
        }
    }

    public void Register(IComponentChangedListener<U> listener)
    {
        _listeners.Add(listener);
    }
}

public abstract class ComponentEventBase<T, V, U> : ComponentEvent<U>, IComponentChangedEvent<T> where T : unmanaged, U where V : ComponentEventSystemBase<T>
{
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

    protected void ValidateMap()
    {
        ValidateMap(_listenerChangedMap);
        ValidateMap(_listenerAddedMap);
        _listeners.RemoveAll(x => x.Equals(null));
    }

    private void ValidateMap<m>(Dictionary<Entity, List<m>> map)
    {
        var remove = new List<Entity>();
        foreach (var key in map.Keys)
        {
            var listeners = map[key];
            listeners.RemoveAll(x => x.Equals(null));
            if (listeners.Count == 0)
                remove.Add(key);
        }
        foreach (var entity in remove)
            map.Remove(entity);
    }

    public void Unregister(Entity entity)
    {
        _listenerChangedMap.Remove(entity);
        _listenerAddedMap.Remove(entity);
    }

    public void OnComponentChanged(T newValue, Entity entity)
    {
        List<IComponentChangedListener<U>> listeners;
        if (_listenerChangedMap.TryGetValue(entity, out listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnComponentChanged(newValue);
            }
        }
        foreach (var listener in _listeners)
        {
            listener.OnComponentChanged(newValue);
        }
    }

    public void OnComponentAdded(T value, Entity entity)
    {
        List<IComponentAddedListener<U>> listeners;
        if (_listenerAddedMap.TryGetValue(entity, out listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnComponentAdded(value);
            }
        }
    }
}

public abstract class ComponentEvent<T, V, U> : ComponentEventBase<T, V, U> where T : unmanaged, IComponentData, U where V : ComponentEventSystem<T>
{

}

public abstract class BufferEvent<T, V, U> : ComponentEventBase<T, V, U> where T : unmanaged, IBufferElementData, IBufferFlag, U where V : BufferEventSystem<T>
{
}
