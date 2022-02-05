using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityInstance))]
public class RegisterEntity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<EntityGroup> _entityLists;
    private EntityInstance _instance;

    private void OnEnable()
    {
        _instance = GetComponent<EntityInstance>();
        foreach (var entity in _entityLists)
            entity.Register(_instance);
    }

    private void OnDisable()
    {
        RemoveFromGroup();
    }

    private void OnDestroy()
    {
        RemoveFromGroup();
    }

    private void RemoveFromGroup()
    {
        foreach (var entity in _entityLists)
            entity.Remove(_instance);
    }
}
