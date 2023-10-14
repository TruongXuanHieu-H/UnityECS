using Unity.Entities;

namespace HelloCube.MoveCube
{
    public class SpeedBaker : Baker<SpeedAuthoring>
    {
        public override void Bake(SpeedAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Speed()
            {
                speed = authoring.speed,
            });
        }
    }
}
