using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Card")]
public class Card : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Resource Resource { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public string Type { get; private set; }
    [field: TextArea]
    [field: SerializeField] public string Description { get; private set; }
}
