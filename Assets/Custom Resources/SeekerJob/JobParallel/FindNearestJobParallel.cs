using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities.UniversalDelegates;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SeekerJob.JobParallel
{
    [BurstCompile]
    public struct FindNearestJobParallel : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float3> seekers;
        [ReadOnly] public NativeArray<float3> targets;
        public NativeArray<float3> nearestTargets;

        public void Execute(int i)
        {
            float3 seeker = seekers[i];
            float nearestDistance = float.MaxValue;

            for (int j = 0; j < targets.Length; j++)
            {
                float3 target = targets[j];
                float distance = math.distancesq(seeker, target);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTargets[i] = targets[j];
                }
            }
        }
    }

}
