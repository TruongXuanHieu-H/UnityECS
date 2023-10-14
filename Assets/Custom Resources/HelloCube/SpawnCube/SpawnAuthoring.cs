using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Scenes;
using Unity.Entities;

namespace HelloCube.SpawnCube
{
    public class SpawnAuthoring : MonoBehaviour
    {
        public GameObject Prefap;
        public int NumberToSpawn;

        class Baker : Baker<SpawnAuthoring>
        {
            public override void Bake(SpawnAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Spawn()
                {
                    Prefap = GetEntity(authoring.Prefap, TransformUsageFlags.None),
                    NumberToSpawn = authoring.NumberToSpawn,
                });
            }
        }
    }

}
