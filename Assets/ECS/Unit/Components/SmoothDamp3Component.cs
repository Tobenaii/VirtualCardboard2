using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SmoothDamp3 : IComponentAuthoring<SmoothDamp3, SmoothDamp3Component>
{
    public float smoothTime;

    public float3 SmoothDamp(float3 current, float3 target, ref float3 currentVelocity, float deltaTime)
    {
        float num = 0f;
        float num2 = 0f;
        float num3 = 0f;
        smoothTime = math.max(0.0001f, smoothTime);
        float num4 = 2f / smoothTime;
        float num5 = num4 * deltaTime;
        float num6 = 1f / (1f + num5 + 0.48f * num5 * num5 + 0.235f * num5 * num5 * num5);
        float num7 = current.x - target.x;
        float num8 = current.y - target.y;
        float num9 = current.z - target.z;
        float3 vector = target;
        float num10 = float.PositiveInfinity * smoothTime;
        float num11 = num10 * num10;
        float num12 = num7 * num7 + num8 * num8 + num9 * num9;
        if (num12 > num11)
        {
            float num13 = (float)math.sqrt(num12);
            num7 = num7 / num13 * num10;
            num8 = num8 / num13 * num10;
            num9 = num9 / num13 * num10;
        }

        target.x = current.x - num7;
        target.y = current.y - num8;
        target.z = current.z - num9;
        float num14 = (currentVelocity.x + num4 * num7) * deltaTime;
        float num15 = (currentVelocity.y + num4 * num8) * deltaTime;
        float num16 = (currentVelocity.z + num4 * num9) * deltaTime;
        currentVelocity.x = (currentVelocity.x - num4 * num14) * num6;
        currentVelocity.y = (currentVelocity.y - num4 * num15) * num6;
        currentVelocity.z = (currentVelocity.z - num4 * num16) * num6;
        num = target.x + (num7 + num14) * num6;
        num2 = target.y + (num8 + num15) * num6;
        num3 = target.z + (num9 + num16) * num6;
        float num17 = vector.x - current.x;
        float num18 = vector.y - current.y;
        float num19 = vector.z - current.z;
        float num20 = num - vector.x;
        float num21 = num2 - vector.y;
        float num22 = num3 - vector.z;
        if (num17 * num20 + num18 * num21 + num19 * num22 > 0f)
        {
            num = vector.x;
            num2 = vector.y;
            num3 = vector.z;
            currentVelocity.x = (num - vector.x) / deltaTime;
            currentVelocity.y = (num2 - vector.y) / deltaTime;
            currentVelocity.z = (num3 - vector.z) / deltaTime;
        }
        return new float3(num, num2, num3);
    }
}

public class SmoothDamp3Component : UnitComponentAuthoring<SmoothDamp3>
{
    [SerializeField] private float _smoothTime;
    protected override SmoothDamp3 AuthorComponent(World world)
    {
        return new SmoothDamp3() { smoothTime = _smoothTime };
    }
}
