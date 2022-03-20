using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Modsys/Entity Data")]
public class EntityData : ScriptableObject
{
    [PropertyOrder(10000)]
    [SerializeField] private EntityDataAuthoring _authoring;

    public Entity Convert(Entity entity, EntityManager dstManager)
    {
        return _authoring.Convert(entity, dstManager);
    }

    private void UpdateChanges(Entity prefab, EntityManager dstManager)
    {
        _authoring.Update(prefab, dstManager);
    }

    public void ValidateComponents()
    {
        if (_authoring.Template != null)
            _authoring.ValidateComponents();
    }

    private void OnValidate()
    {
        if (_authoring != null)
            _authoring.Template?.Register(this);

        if (!Application.isPlaying)
            return;
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity prefab;
        _prefabs.TryGetValue(this, out prefab);
        if (prefab != default)
            UpdateChanges(prefab, dstManager);
    }

    public Entity GetPrefab(EntityManager dstManager)
    {
        Entity prefab;
        if (_prefabs.TryGetValue(this, out prefab))
        {
            if (prefab == default)
            {
                prefab = _authoring.CreatePrefab(dstManager, this.name);
                _prefabs[this] = prefab;
            }
            return _prefabs[this];
        }
        else
        {
            prefab = _authoring.CreatePrefab(dstManager, this.name);
            _prefabs.Add(this, prefab);
            return prefab;
        }
    }

    //TODO: yeah idk about this lmao

    private static Dictionary<EntityData, Entity> _prefabs = new Dictionary<EntityData, Entity>();

    [InitializeOnLoadAttribute]
    public static class PlayModeStateChangedExample
    {
        static PlayModeStateChangedExample()
        {
            EditorApplication.playModeStateChanged += ModeStateChanged;
        }

        private static void ModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                foreach (var key in _prefabs.Keys.ToList())
                    _prefabs[key] = default;
            }
        }
    }
}