using Unity.Entities;

namespace Tank
{
    public struct Turret : IComponentData
    {
        public Entity bullet;
        public Entity bulletSpawner;
    }
}