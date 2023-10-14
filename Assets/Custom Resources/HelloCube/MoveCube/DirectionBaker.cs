using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace HelloCube.MoveCube
{
    public class DirectionBaker : Baker<DirectionAuthoring>
    {
        public override void Bake(DirectionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Direction()
            {
                direction = authoring.direction,
            });

        }
    }
}
