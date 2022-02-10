using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Unit Data")]
public class UnitData : Archetype
{
    protected override Type authoringType => typeof(UnitComponentAuthoring<>);

    protected override Type bufferAuthoringType => typeof(UnitBufferComponentAuthoring<>);
}
