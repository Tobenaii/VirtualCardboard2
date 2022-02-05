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
    [SerializeField] private Attribute _attribute;
    [ListDrawerSettings(HideAddButton = true)]
    [SerializeField] private List<AttributeResolverCheck> _resolverChecks;

    public void Resolve(EntityInstance dealer, EntityInstance receiver, float amount)
    {
        foreach (var check in _resolverChecks)
            amount = check.Resolve(dealer, receiver, amount);
        _attribute[receiver] = _attribute[receiver] + amount;
        //receiver.SetAttribute(_attribute, receiver.GetAttribute(_attribute) + amount);
    }

    [Button("Add Check")]
    private void AddCheck()
    {
        var checks = UnityTypeCacheUtility.GetTypesDerivedFrom(typeof(AttributeResolverCheck)).ToList();
        GenericSelector<Type> selector = new GenericSelector<Type>(checks);
        selector.SelectionConfirmed += (x) => AddCheck(x.First());
        selector.ShowInPopup();
    }

    private void AddCheck(Type checkType)
    {
        var check = ScriptableObject.CreateInstance(checkType) as AttributeResolverCheck;
        check.name = checkType.Name;
        _resolverChecks.Add(check);
        AssetDatabase.AddObjectToAsset(check, this);
        AssetDatabase.Refresh();
    }
}
