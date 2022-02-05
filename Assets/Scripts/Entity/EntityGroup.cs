using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Entity/List")]
public class EntityGroup : ScriptableObject
{
    private List<EntityInstance> _entityInstances = new List<EntityInstance>();

    public EntityInstance this[int index] => _entityInstances[index];

    public int Count => _entityInstances.Count;

    public void Register(EntityInstance instance)
    {
        _entityInstances.Add(instance);
    }

    public void Remove(EntityInstance instance)
    {
        _entityInstances.Remove(instance);
    }
}
