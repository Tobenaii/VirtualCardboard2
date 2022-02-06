using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/UnityHook")]
public class UnityHook : Archetype
{
    protected override Type authoringType => typeof(UnityHookAuthoring<>);
}
