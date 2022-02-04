using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CardActionListDrawer : OdinValueDrawer<CardActionList> 
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var list = Property.NextProperty();
        list.Draw();

        if (GUILayout.Button("Add Action"))
            AddAction(this.Property.SerializationRoot.ValueEntry.WeakSmartValue as UnityEngine.Object, list.ValueEntry.WeakSmartValue as List<CardActionData>);
    }

    private void AddAction(UnityEngine.Object obj, List<CardActionData> actionList)
    {
        List<CardAction> actions = AssetDatabase.FindAssets($"t: {typeof(CardAction).Name}").ToList()
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CardAction>)
                .ToList();
        GenericSelector<CardAction> selector = new GenericSelector<CardAction>("Card Actions", actions);
        selector.SelectionConfirmed += (action) => CreateActionData(action.First(), obj, actionList);
        selector.ShowInPopup();
    }

    private void CreateActionData(CardAction action, UnityEngine.Object obj, List<CardActionData> actionList)
    {
        var type = TypeCache.GetTypesDerivedFrom<CardActionData>().FirstOrDefault(x => TypeHasGeneric(x, action.GetType()));
        if (type != null)
            AddDataActionType(type, action, obj, actionList);
        else
            AddDataActionType(typeof(CardActionData), action, obj, actionList);
    }

    private void AddDataActionType(Type type, CardAction action, UnityEngine.Object obj, List<CardActionData> actionList)
    {
        var data = ScriptableObject.CreateInstance(type) as CardActionData;
        data.RegisterCardAction(action);
        data.name = action.name;
        actionList.Add(data);
        AssetDatabase.AddObjectToAsset(data, AssetDatabase.GetAssetPath(obj));
    }

    private bool TypeHasGeneric(Type type, Type generic)
    {
        if (type.GenericTypeArguments.Count() != 0)
            if (type.GenericTypeArguments[0] == generic) return true;
        if (type.BaseType == null)
            return false;
        return TypeHasGeneric(type.BaseType, generic);
    }
}
