using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class LevelData : IComponentData
{
    [SerializeField] public int currentLevel;
}
