using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IComponentListener<T> where T : struct, IComponentData
{
    public void OnComponentChanged(T prevValue, T newValue);
}

public interface IComponentEvent<T> where T : struct, IComponentData
{
    public void OnComponentChanged(T prevValue, T newValue, Entity entity);
}

public abstract class ComponentChangeEvent<T> : ScriptableObject, IComponentEvent<T> where T : struct, IComponentData
{
    private Dictionary<Entity, List<IComponentListener<T>>> _listenerMap = new Dictionary<Entity, List<IComponentListener<T>>>();

    public void Clear()
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

    public void OnComponentChanged(T prevValue, T newValue, Entity entity)
    {
        List<IComponentListener<T>> listeners;
        if (_listenerMap.TryGetValue(entity, out listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnComponentChanged(prevValue, newValue);
            }
        }

    }
}
