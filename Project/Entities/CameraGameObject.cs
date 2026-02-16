using Project.Components;
using UmbrellaToolsKit;

namespace Project.Entities
{
    public class CameraGameObject : GameObject
    {
        public override void Start()
        {
            AddComponent<CameraComponent>();
        }
    }
}
