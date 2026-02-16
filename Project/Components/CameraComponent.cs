using UmbrellaToolsKit;

namespace Project.Components
{
    public class CameraComponent : Component
    {
        private CameraController _camera;

        public override void Start()
        {
            _camera = GameObject.Scene.Camera;
            GameObject.Position = GameObject.Scene.Sizes.ToVector2() / 2f;
            GameObject.Position = GameObject.Position.Truncate();
        }

        public override void Update(float deltaTime)
        {
            if (_camera is null) return;

            _camera.Position = GameObject.Position;
        }
    }
}
