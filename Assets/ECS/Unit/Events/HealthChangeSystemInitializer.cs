using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthChangeSystemInitializer : MonoBehaviour
{
    [SerializeField] private HealthChangeEvent _event;
    private void Awake()
    {
        _event.Clear();
        World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<HealthChangeEventSystem>().healthChangedEvent = _event;
    }
}
