using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube.MoveCube
{
    public partial struct SpeedSystem : ISystem
    {
        void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, speed, entity) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<Speed>>().WithEntityAccess())
            {
                if (transform.ValueRO.Position.x > 8 || transform.ValueRO.Position.x < -8)
                {
                    speed.ValueRW.speed = -speed.ValueRO.speed;
                }
            }
        }
    }
}
