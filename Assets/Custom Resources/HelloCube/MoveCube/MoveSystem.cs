using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube.MoveCube
{
    public partial struct MoveSystem : ISystem
    {
        public bool IsUseJob;

        void OnCreate(ref SystemState state)
        {

        }

        void OnUpdate(ref SystemState state)
        {
            if (!IsUseJob)
            {
                foreach (var (transform, speed, direction, entity) in
                    SystemAPI.Query<RefRW<LocalTransform>, RefRO<Speed>, RefRO<Direction>>().WithEntityAccess())
                {
                    transform.ValueRW.Position += (new float3(
                        SystemAPI.Time.DeltaTime * speed.ValueRO.speed * direction.ValueRO.direction.x,
                        SystemAPI.Time.DeltaTime * speed.ValueRO.speed * direction.ValueRO.direction.y,
                        SystemAPI.Time.DeltaTime * speed.ValueRO.speed * direction.ValueRO.direction.z));
                }
            } else
            {
                NativeList<RefRW<LocalTransform>> transforms = new NativeList<RefRW<LocalTransform>>(Allocator.TempJob);
                NativeList<RefRO<Speed>> speeds = new NativeList<RefRO<Speed>>(Allocator.TempJob);
                NativeList<RefRO<Direction>> directions = new NativeList<RefRO<Direction>>(Allocator.TempJob);
                foreach (var (transform, speed, direction, entity) in
                    SystemAPI.Query<RefRW<LocalTransform>, RefRO<Speed>, RefRO<Direction>>().WithEntityAccess())
                {
                    transforms.Add(transform);
                    speeds.Add(speed);
                    directions.Add(direction);
                }

            }
        }
    }

    [BurstCompile]
    public struct MoveJob : IJobParallelFor
    {
        public NativeArray<float3> positions;
        public NativeArray<float> speed;
        public NativeArray<float3> directions;
        public float deltaTime;
        public void Execute(int index)
        {
            positions[index] += new float3(speed[index] * directions[index].x * deltaTime,
                speed[index] * directions[index].y * deltaTime,
                speed[index] * directions[index].z * deltaTime);
        }
    }
}
