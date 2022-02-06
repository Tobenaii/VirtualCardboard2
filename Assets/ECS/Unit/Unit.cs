using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Unit")]
public class Unit : Archetype
{
    protected override Type authoringType => typeof(UnitComponentAuthoring<>);

    protected override Type bufferAuthoringType => typeof(UnitBufferComponentAuthoring<>);
}
