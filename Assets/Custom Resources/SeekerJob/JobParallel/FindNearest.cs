using SeekerJob.Job;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SeekerJob.JobParallel
{
    public class FindNearest : MonoBehaviour
    {
        [SerializeField, ShowInInspector] public NativeArray<float3> seekers;
        [SerializeField, ShowInInspector] public NativeArray<float3> targets;
        [SerializeField, ShowInInspector] public NativeArray<float3> nearest;

        private void Start()
        {
            InitData();
        }
        private void Update()
        {
            ClearData();
            FetchData();

            FindNearestJobParallel findJob = new FindNearestJobParallel
            {
                seekers = seekers,
                targets = targets,
                nearestTargets = nearest,
            };
            findJob.Schedule(seekers.Length, 100).Complete();

            for (int i = 0; i < seekers.Length; i++)
            {
                Debug.DrawLine(seekers[i], nearest[i]);
            }
        }
        private void OnDestroy()
        {
            DisposeData();
        }

        private void InitData()
        {
            seekers = new NativeArray<float3>(Spawner.instance.spawnedSeekers.Count, Allocator.Persistent);
            targets = new NativeArray<float3>(Spawner.instance.spawnedTargets.Count, Allocator.Persistent);
            nearest = new NativeArray<float3>(Spawner.instance.spawnedSeekers.Count, Allocator.Persistent);
        }

        private void FetchData()
        {
            var spawnedSeekers = Spawner.instance.spawnedSeekers;
            for (int i = 0; i < spawnedSeekers.Count; i++)
            {
                seekers[i] = spawnedSeekers[i].transform.position;
            }

            var spawnedTargets = Spawner.instance.spawnedTargets;
            for (int i = 0; i < spawnedTargets.Count; i++)
            {
                targets[i] = spawnedTargets[i].transform.position;
            }
        }
        private void ClearData()
        {
            
        }
        private void DisposeData()
        {
            seekers.Dispose();
            targets.Dispose();
            nearest.Dispose();
        }
    }

}
