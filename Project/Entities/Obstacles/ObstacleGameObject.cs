using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities.Obstacles
{
    public class ObstacleGameObject : GameObject
    {
        private SpriteComponent _spriteComponent;

        public override void Start()
        {
            _spriteComponent = AddComponent<SpriteComponent>();
            _spriteComponent.SetAtlas("debug sprite");
        }
    }
}
