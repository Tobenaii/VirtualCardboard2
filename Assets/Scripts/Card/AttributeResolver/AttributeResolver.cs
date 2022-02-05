using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Attribute Resolver")]
public class AttributeResolver : ScriptableObject
{
    public enum ResolverType { Add, Subtract }

    [SerializeField] private ResolverType _resolverType;
    [SerializeField] private Attribute _attribute;
    [ListDrawerSettings(HideAddButton = true)]
    [SerializeField] private List<AttributeResolverPhase> _resolverPhases;

    public void Resolve(EntityInstance dealer, EntityInstance receiver, float amount)
    {
        foreach (var check in _resolverPhases)
            amount = check.Resolve(dealer, receiver, amount);
        _attribute[receiver] = _attribute[receiver] + amount * (_resolverType == ResolverType.Add ? 1 : -1);
        //receiver.SetAttribute(_attribute, receiver.GetAttribute(_attribute) + amount);
    }

    [Button("Add Check")]
    private void AddCheck()
    {
        var checks = UnityTypeCacheUtility.GetTypesDerivedFrom(typeof(AttributeResolverPhase)).ToList();
        GenericSelector<Type> selector = new GenericSelector<Type>(checks);
        selector.SelectionConfirmed += (x) => AddCheck(x.First());
        selector.ShowInPopup();
    }

    private void AddCheck(Type checkType)
    {
        var check = ScriptableObject.CreateInstance(checkType) as AttributeResolverPhase;
        check.name = checkType.Name;
        _resolverPhases.Add(check);
        AssetDatabase.AddObjectToAsset(check, this);
        AssetDatabase.Refresh();
    }
}
