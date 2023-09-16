using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using Unity.Burst;

public partial struct LevelManageSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var entity in SystemAPI.Query<LevelData, StatData>())
        {
            entity.Item1.currentLevel++;
            entity.Item2.attackPower = entity.Item1.currentLevel * 2;
            entity.Item2.healthPoint = entity.Item1.currentLevel * 10;
        }
    }
}
