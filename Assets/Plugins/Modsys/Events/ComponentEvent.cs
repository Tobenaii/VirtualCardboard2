using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IComponentListener<T>
{
    public void OnComponentChanged(T value);
}

public interface IComponentEvent<T>
{
    public void OnComponentChanged(T value, Entity entity);
}

public abstract class ComponentEvent : ScriptableObject 
{
    public abstract void Init();
    public abstract void Disable();
}

public abstract class ComponentEvent<U> : ComponentEvent
{
    protected Dictionary<Entity, List<IComponentListener<U>>> _listenerMap = new Dictionary<Entity, List<IComponentListener<U>>>();
    protected List<IComponentListener<U>> _listeners = new List<IComponentListener<U>>();

    public void Register(Entity entity, IComponentListener<U> listener)
    {
        List<IComponentListener<U>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
            listeners.Add(listener);
        else
        {
            listeners = new List<IComponentListener<U>>();
            listeners.Add(listener);
            _listenerMap.Add(entity, listeners);
        }
    }

    public void Register(IComponentListener<U> listener)
    {
        _listeners.Add(listener);
    }
}

public abstract class ComponentEventBase<T, V, U> : ComponentEvent<U>, IComponentEvent<T> where T : unmanaged, U where V : ComponentEventSystemBase<T>
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

    public void OnComponentChanged(T newValue, Entity entity)
    {
        List<IComponentListener<U>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
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

    protected void ValidateMap()
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
        _listeners.RemoveAll(x => x.Equals(null));
            
    }

    public void Unregister(Entity entity)
    {
        _listenerMap.Remove(entity);
    }
}

public abstract class ComponentEvent<T, V, U> : ComponentEventBase<T, V, U> where T : unmanaged, U where V : ComponentEventSystem<T> where U : IComponentData
{

}

public abstract class BufferEvent<T, V, U> : ComponentEventBase<T, V, U> where T : unmanaged, U where V : BufferEventSystem<T> where U : IBufferElementData
{
}
