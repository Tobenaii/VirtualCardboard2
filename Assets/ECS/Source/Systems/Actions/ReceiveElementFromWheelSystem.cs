using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(ActionSystemGroup))]
public class ReceiveElementFromWheelSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        int randomElement = UnityEngine.Random.Range(0, 100);
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Dealer dealer, in ReceiveElementFromWheel action) =>
        {
            var wheelBuffer = GetBufferFromEntity<ElementWheel>(false)[dealer.Entity];
            var elementBuffer = GetBufferFromEntity<Element>(false)[dealer.Entity];

            ElementWheel winningElement = wheelBuffer[0];
            float distribution = 0;
            foreach (var element in wheelBuffer)
            {
                distribution += element.Percentage;
                if (distribution > randomElement)
                {
                    winningElement = element;
                    break;
                }
                    
            }
            var newWinningPercentage = winningElement.Percentage * 0.5f;

            for (int i = 0; i < wheelBuffer.Length; i++)
            {
                var element = wheelBuffer[i];
                var targetElement = elementBuffer[(int)element.Type];
                if (element.Type == winningElement.Type)
                {
                    targetElement.Count++;
                    element.Percentage = newWinningPercentage;
                }
                else
                    element.Percentage += newWinningPercentage / 2;
                wheelBuffer[i] = element;
                elementBuffer[(int)element.Type] = targetElement;
            }

            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
