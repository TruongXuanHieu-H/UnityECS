using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SeekerJob.Job
{
    public class FindNearest : MonoBehaviour
    {
        [SerializeField] public NativeList<float3> seekers;
        [SerializeField] public NativeList<float3> targets;
        [SerializeField] public NativeList<float3> nearest;

        private void Start()
        {
            InitData();
        }
        private void Update()
        {
            ClearData();
            FetchData();

            FindNearestJob findJob = new FindNearestJob
            {
                seekers = seekers,
                targets = targets,
                nearestTargets = nearest,
            };
            findJob.Schedule().Complete();

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
            seekers = new NativeList<float3>(Allocator.Persistent);
            targets = new NativeList<float3>(Allocator.Persistent);
            nearest = new NativeList<float3>(Allocator.Persistent);
        }

        private void FetchData()
        {
            foreach (var seeker in Spawner.instance.spawnedSeekers)
            {
                seekers.Add(seeker.transform.position);
            }

            foreach (var target in Spawner.instance.spawnedTargets)
            {
                targets.Add(target.transform.position);
                nearest.Add(target.transform.position);
            }
        }
        private void ClearData()
        {
            seekers.Clear();
            targets.Clear();
            nearest.Clear();
        }
        private void DisposeData()
        {
            seekers.Dispose();
            targets.Dispose();
            nearest.Dispose();
        }
    }
}
