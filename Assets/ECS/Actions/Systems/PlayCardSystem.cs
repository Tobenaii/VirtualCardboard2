using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

//[UpdateInGroup(typeof(ActionResolverGroup))]
//public class PlayCardSystem : SystemBase
//{
//    private BeginInitializationEntityCommandBufferSystem _endSimulationEcbSystem;

//    protected override void OnCreate()
//    {
//        _endSimulationEcbSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
//    }

//    protected override void OnUpdate()
//    {
//        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

//        Entities.ForEach((int entityInQueryIndex, Entity entity, in PlayCard playCard) =>
//        {
//            if (playCard.Status != IStatusMessage.StatusType.Failed)
//            {
//                var dealer = playCard.Dealer;
//                var target = GetComponentDataFromEntity<SingleTarget>(true)[dealer];
//                var card = ecb.Instantiate(entityInQueryIndex, playCard.Card);
//                ecb.SetComponent<Target>(entityInQueryIndex, card, new Target() { dealer = dealer, target = target.target });
//            }
//            ecb.DestroyEntity(entityInQueryIndex, entity);

//        }).ScheduleParallel();
//        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
//    }
//}
