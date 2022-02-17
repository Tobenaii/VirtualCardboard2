using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[System.Serializable] [InlineProperty]
public class EntityRef
{
    [HorizontalGroup(MaxWidth = 1.0f)]
    [HideLabel]
    [SerializeField] private EntityGroup _entityList;
    [HorizontalGroup(MaxWidth = 0.1f)] [HideLabel]
    [SerializeField] private int _key;

    public Entity Entity
    {
        get
        {
            return _entityList[_key];
        }
        set
        {
            _entityList.Register(_key, value);
        }
    }

    public void Unregister()
    {
        _entityList.Unregister(_key);
    }
}

[CreateAssetMenu(menuName = "Modsys/Entity Group")]
public class EntityGroup : ScriptableObject
{
    [SerializeField] private int _initialCapacity;
    [ShowInInspector][ReadOnly] private Dictionary<int, Entity?> _entities = new Dictionary<int, Entity?>();

    public Entity this[int key]
    {
        get
        {
            if (!_entities[key].HasValue)
                throw new KeyNotFoundException();
            return _entities[key].Value;
        }
    }

    public void Register(int key, Entity entity)
    {
        if (_entities.ContainsKey(key))
        {
            _entities.Remove(key);
            _entities.Add(key, entity);
        }
        else
            _entities.Add(key, entity);
    }

    public void Unregister(int key)
    {
        _entities.Remove(key);
    }
}
