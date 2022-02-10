using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public abstract class Archetype : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private List<ModEntity> _entities;
    protected abstract Type authoringType { get; }
    protected abstract Type bufferAuthoringType { get; }
    [ListDrawerSettings(HideAddButton = true, CustomRemoveElementFunction = "RemoveComponent")]
    [SerializeField][HideReferenceObjectPicker] protected List<ReadOnlyComponent> _components = new List<ReadOnlyComponent>();
    public IEnumerable<ComponentAuthoringBase> Components => _components.Select(x => x.Component);
    private Entity _prefab;

    [Button]
    private void AddComponent()
    {
        IEnumerable<Type> list = TypeCache.GetTypesDerivedFrom(authoringType);
        if (bufferAuthoringType != null)
        {
            list = list.Concat(TypeCache.GetTypesDerivedFrom(bufferAuthoringType));
        }
        list = list.Where(x => !x.IsAbstract);
        var selector = new GenericSelector<Type>("Component Selector", list);

        selector.SelectionConfirmed += selection =>
        {
            var type = selection.FirstOrDefault();
            if (type == null)
                return;
            var instance = ScriptableObject.CreateInstance(type) as ComponentAuthoringBase;
            AssetDatabase.AddObjectToAsset(instance, this);
            AssetDatabase.Refresh();
            _components.Add(instance);
        };
        var window = selector.ShowInPopup();
    }

    public void Register(ModEntity modEntity)
    {
        if (!_entities.Contains(modEntity))
            _entities.Add(modEntity);
    }

    private void RemoveComponent(ReadOnlyComponent component)
    {
        DestroyImmediate(component.Component, true);
        _components.Remove(component);
    }

    public void OnBeforeSerialize()
    {
        //Remove all subassets that shouldn't exist (the custom remove should handle this for most cases)
        foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this)))
        {
            if (asset == this)
                continue;
            if (asset == null || !_components.Select(x => x.Component.GetType()).Contains(asset.GetType()))
                UnityEngine.Object.DestroyImmediate(asset, true);
        }
        foreach (var entity in _entities.ToList())
        {
            if (entity == null)
                _entities.Remove(entity);
            else
                entity.ValidateComponents();
        }
    }

    public void OnAfterDeserialize()
    {
    }
}
