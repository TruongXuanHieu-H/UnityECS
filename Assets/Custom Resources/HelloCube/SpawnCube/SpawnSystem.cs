using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using HelloCube.MoveCube;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube.SpawnCube
{
    public partial struct SpawnSystem : ISystem
    {
        uint updateCounter;
        bool isSpawned;

        [BurstCompile]
        void OnCreate(ref SystemState state)
        {
            isSpawned = false;
            state.RequireForUpdate<Spawn>();
        }

        [BurstCompile]
        void OnUpdate(ref SystemState state)
        {
            if (isSpawned)
            {               
                return;
            }

            isSpawned = true;

            var spawn = SystemAPI.GetSingleton<Spawn>();
            var instances = state.EntityManager.Instantiate(spawn.Prefap, spawn.NumberToSpawn, Allocator.Persistent);
            var random = Unity.Mathematics.Random.CreateFromIndex(updateCounter++);

            foreach (var entity in instances)
            {
                var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                transform.ValueRW.Position = new float3(
                    random.NextFloat() * 8 * (random.NextBool() ? 1 : -1),
                    random.NextFloat() * 5 * (random.NextBool() ? 1 : -1), 0);

                var speed = SystemAPI.GetComponentRW<Speed>(entity);
                float s = random.NextInt() % 8;
                speed.ValueRW.speed = (s == 0 ? 1 : s);

                var direction = SystemAPI.GetComponentRW<Direction>(entity);
                float3 dir = new float3((random.NextBool() ? 1 : -1), 0, (random.NextBool() ? 1 : -1));
                direction.ValueRW.direction = dir;
            }
        }
    }

}
