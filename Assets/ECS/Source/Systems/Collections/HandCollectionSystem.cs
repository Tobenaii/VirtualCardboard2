using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(HandCollectionSystem.GenericContainerJob))]
public class HandCollectionSystem : CollectionContainerSystem<Hand, HandCards> { }