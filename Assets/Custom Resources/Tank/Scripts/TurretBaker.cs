using Unity.Entities;

namespace Tank
{
    public class TurretBaker : Baker<TurretAuthoring>
    {
        public override void Bake(TurretAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Turret()
            {
                bullet = GetEntity(authoring.bullet, TransformUsageFlags.Dynamic),
                bulletSpawner = GetEntity(authoring.bulletSpawner, TransformUsageFlags.Dynamic),
            });
            
        }
    }
}