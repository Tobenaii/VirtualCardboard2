using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameplayActionSelector : OdinSelector<GameplayObject>
{
    protected override void BuildSelectionTree(OdinMenuTree tree)
    {
        tree.Config.DrawSearchToolbar = true;
        tree.Selection.SupportsMultiSelect = false;

        List<GameplayObject> actions = AssetDatabase.FindAssets($"t: {typeof(GameplayObject).Name}").ToList()
                     .Select(AssetDatabase.GUIDToAssetPath)
                     .Select(AssetDatabase.LoadAssetAtPath<GameplayObject>)
                     .ToList();
        foreach (var action in actions)
        {
            tree.Add(action.name, action);
        }
    }
}
