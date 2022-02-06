using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Card")]
public class Card : ArchetypePrefab
{
    protected override Type authoringType => typeof(CardEffectAuthoring<>);

}
