using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InlineProperty] [HideLabel] [System.Serializable]
public class CardActionList
{
    [SerializeField]
    [ListDrawerSettings(HideAddButton = true)]
    [AssetSelector] [InlineEditor]
    private List<CardActionData> _actions;
    public List<CardActionData> Actions => _actions;
}

[CreateAssetMenu(menuName = "VC2/Card")]
public class Card : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public string Type { get; private set; }
    [field: TextArea]
    [field: SerializeField] public string Description { get; private set; }

    [SerializeField] private CardActionList _actions;

    public void Execute(EntityInstance player)
    {
        foreach (var action in _actions.Actions)
            action.Execute(player);
    }
}





