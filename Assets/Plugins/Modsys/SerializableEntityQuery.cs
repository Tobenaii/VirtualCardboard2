using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SerializableEntityQuery<T> where T : struct, IComponentData
{
    [System.Serializable][InlineEditor]
    private class ReadOnlyStringType
    {
        [SerializeField][HideInInspector] private string _fullName
            ;
        [SerializeField] [HideLabel] [ReadOnly] private string _name;

        public string Name => _name;
        public Type Type => typeof(T).Assembly.GetType(_fullName);
        public static implicit operator ReadOnlyStringType(Type type)
        {
            return new ReadOnlyStringType()
            {
                _fullName = type.FullName,
                _name = type.Name
            };
        }
    }

    private ComponentType[] _componentTypes;
    
    public void Init()
    {
        _componentTypes = _components.Select(x => new ComponentType(x.Type, ComponentType.AccessMode.ReadOnly)).Append(new ComponentType(typeof(T), ComponentType.AccessMode.ReadOnly)).ToArray();
    }

    public T Query(int entityIndex)
    {
        var mng = World.DefaultGameObjectInjectionWorld.EntityManager;
        var healthEntities = mng.CreateEntityQuery(_componentTypes).ToComponentDataArray<T>(Unity.Collections.Allocator.Temp);
        return healthEntities[entityIndex];
    }

    [ListDrawerSettings(CustomAddFunction = "AddComponent")]
    [SerializeField] private List<ReadOnlyStringType> _components = new List<ReadOnlyStringType>();


    private void AddComponent()
    {
        var selector = new GenericSelector<Type>("Component Selector", TypeCache.GetTypesDerivedFrom<IComponentData>().Where(x => !x.IsAbstract));
        selector.SelectionConfirmed += selection =>
        {
            var type = selection.FirstOrDefault();
            if (type == null)
                return;
            _components.Add(type);
        };
        var window = selector.ShowInPopup();
    }
}
