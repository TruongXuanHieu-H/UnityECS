using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SeekerJob.Job
{
    [BurstCompile]
    public struct FindNearestJob : IJob
    {
        [ReadOnly] public NativeList<float3> seekers;
        [ReadOnly] public NativeList<float3> targets;
        public NativeList<float3> nearestTargets;

        public void Execute()
        {
            for (int i = 0; i < seekers.Length; i++)
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
}
