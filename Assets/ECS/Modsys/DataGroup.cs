using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InlineEditor]
public abstract class DataGroupElement : ScriptableObject
{
    public int Index { get; private set; }

    public void SetIndex(int index)
    {
        Index = index;
    }
}

//TODO: Move this to Modsys
public abstract class DataGroup<T> : SerializedScriptableObject, IEnumerable<T> where T : DataGroupElement
{
    [ListDrawerSettings(HideAddButton = true, Expanded = true, CustomRemoveElementFunction = "RemoveElement")]
    [SerializeField] private List<T> _data;
    public int Count => _data.Count;

    public T this[int key]
    {
        get => _data[key];
    }

    private void OnValidate()
    {
        SetIndices();
    }

    protected override void OnBeforeSerialize()
    {
        SetIndices();
    }

    private void SetIndices()
    {
        for (int i = 0; i < _data.Count; i++)
            _data[i].SetIndex(i);
    }

    private void RemoveElement(T data)
    {
        ScriptableObject.DestroyImmediate(data, true);
        _data.Remove(data);
    }

    [Button("Add Data")]
    private void AddElement()
    {
        var instance = ScriptableObject.CreateInstance<T>();
        instance.SetIndex(_data.Count);
        _data.Add(instance);
        AssetDatabase.AddObjectToAsset(instance, this);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _data.GetEnumerator();
    }
}
