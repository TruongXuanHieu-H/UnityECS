using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace HelloCube.SpawnCube
{
    public partial struct Spawn : IComponentData
    {
        public Entity Prefap;
        public int NumberToSpawn;
    }
}
