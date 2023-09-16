using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public class Test : MonoBehaviour
{
    [SerializeField] protected EntityManager entityManager;
    private void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype archerType = entityManager.CreateArchetype(typeof(LevelData), typeof(StatData));

        NativeArray<Entity> entityArray = new NativeArray<Entity>(1000, Allocator.Temp);

        entityManager.CreateEntity(archerType, entityArray);
        foreach (Entity entity in entityArray)
        {
            entityManager.SetComponentData(entity, new LevelData());
            entityManager.SetComponentData(entity, new StatData());
        }
    }
}
