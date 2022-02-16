using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IComponentListener<T>
{
    public void OnComponentChanged(T value);
}

public interface IComponentEvent<T, K>
{
    public void OnComponentChanged(T value, Entity entity, K type);
}

public abstract class ComponentEvent : ScriptableObject 
{
    public abstract void Init();
    public abstract void Disable();
}

public abstract class ComponentEvent<T, K> : ComponentEvent where T : ITypeEvent where K : Enum 
{
    protected Dictionary<K, Dictionary<Entity, List<IComponentListener<T>>>> _typeListenerMap = new Dictionary<K, Dictionary<Entity, List<IComponentListener<T>>>>();
    protected List<IComponentListener<T>> _listeners = new List<IComponentListener<T>>();

    public void Register(Entity entity, K type, IComponentListener<T> listener)
    {
        Dictionary<Entity, List<IComponentListener<T>>> listenerMap;
        if (!_typeListenerMap.TryGetValue(type, out listenerMap))
        {
            listenerMap = new Dictionary<Entity,List<IComponentListener<T>>>();
            _typeListenerMap.Add(type, listenerMap);
        }    

        List<IComponentListener<T>> listeners;
        if (listenerMap.TryGetValue(entity, out listeners))
            listeners.Add(listener);
        else
        {
            listeners = new List<IComponentListener<T>>();
            listeners.Add(listener);
            listenerMap.Add(entity, listeners);
        }
    }

    public void Register(IComponentListener<T> listener)
    {
        _listeners.Add(listener);
    }
}

public abstract class ComponentEventBase<T, V, K> : ComponentEvent<T, K>, IComponentEvent<T, K> where K : Enum where T : unmanaged, ITypeEvent where V : ComponentEventSystemBase<T, K>
{
    public override void Init()
    {
        ValidateMap();
        var world = World.DefaultGameObjectInjectionWorld;
        var system = world.GetOrCreateSystem<V>();
        system.componentChangedEvent = this;
    }

    public override void Disable()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
            return;
        var system = world.GetExistingSystem<V>();
        system.Enabled = false;
    }

    public void OnComponentChanged(T newValue, Entity entity, K type)
    {
        Dictionary<Entity, List<IComponentListener<T>>> listenerMap;
        if (_typeListenerMap.TryGetValue(type, out listenerMap))
        {
            List<IComponentListener<T>> listeners;
            if (listenerMap.TryGetValue(entity, out listeners))
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

    }

    protected void ValidateMap()
    {
        foreach (var key in _typeListenerMap.Keys)
        {
            var remove = new List<Entity>();
            var listenerMap = _typeListenerMap[key];
            foreach (var key2 in listenerMap.Keys)
            {
                var listeners = listenerMap[key2];
                listeners.RemoveAll(x => x.Equals(null));
                if (listeners.Count == 0)
                    remove.Add(key2);
            }
            foreach (var entity in remove)
                listenerMap.Remove(entity);
        }
        _listeners.RemoveAll(x => x.Equals(null));
    }

    public void Unregister(Entity entity, K type)
    {
        _typeListenerMap[type].Remove(entity);
    }
}

public interface ITypeEvent { }

public interface ITypeBufferElementEvent<T> : ITypeEvent where T : Enum
{
    public T Type { get; }
}

public interface ITypeComponentEvent<T> : ITypeEvent where T : Enum
{
    public T Type { get; }
}


public abstract class ComponentEvent<T, V, K> : ComponentEventBase<T, V, K>
    where T : unmanaged, ITypeComponentEvent<K>, IComponentData where V : ComponentEventSystem<T, K> where K : Enum
{

}

public abstract class BufferEvent<T, V, K> : ComponentEventBase<T, V, K>
    where T : unmanaged, ITypeBufferElementEvent<K>, IBufferElementData where V : BufferEventSystem<T, K> where K : Enum
{
}
