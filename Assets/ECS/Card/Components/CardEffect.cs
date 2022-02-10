using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Card Effect")]
public class CardEffect : Archetype
{
    protected override Type authoringType => typeof(CardEffectAuthoring<>);

    protected override Type bufferAuthoringType => null;
}
