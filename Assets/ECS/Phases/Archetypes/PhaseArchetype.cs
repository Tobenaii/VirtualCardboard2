using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Phase Archetype")]
public class PhaseArchetype : Archetype
{
    protected override Type authoringType => typeof(PhaseComponentAuthoring<>);

    protected override Type bufferAuthoringType => null;
}
