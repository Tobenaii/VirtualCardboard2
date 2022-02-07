using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEventInitializer : MonoBehaviour
{
    [SerializeField] private List<ComponentEvent> _events;

    private void OnEnable()
    {
        foreach (var e in _events)
            e.Init();
    }

    private void OnDisable()
    {
        foreach (var e in _events)
            e.Disable();
    }
}
