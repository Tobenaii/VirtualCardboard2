using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Card Data")]
public class CardData : ArchetypePrefab
{
    protected override Type authoringType => typeof(CardEffectAuthoring<>);

}
