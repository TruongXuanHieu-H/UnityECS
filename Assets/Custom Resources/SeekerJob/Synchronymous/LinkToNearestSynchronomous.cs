using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeekerJob.Synchronymous
{
    public class LinkToNearestSynchronomous : MonoBehaviour
    {
        public void UpdateLink()
        {
            if (Spawner.instance.spawnedTargets.Count == 0)
                return;

            Target nearestTarget = Spawner.instance.spawnedTargets[0];
            float nearestDistance = Vector3.Distance(this.transform.position, nearestTarget.transform.position);

            foreach (Target target in Spawner.instance.spawnedTargets)
            {
                float distance = Vector3.Distance(this.transform.position, target.transform.position);
                if (distance < nearestDistance)
                {
                    nearestTarget = target;
                    nearestDistance = distance;
                }
            }
            Debug.DrawLine(transform.position, nearestTarget.transform.position);
        }

        private void Update()
        {
            UpdateLink();
        }
    }
}
