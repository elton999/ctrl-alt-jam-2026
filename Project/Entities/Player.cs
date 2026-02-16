using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Entities
{
    public class Player : GameObject
    {
        private SpriteComponent _spriteComponent;
        public override void Start()
        {
            _spriteComponent = AddComponent<SpriteComponent>();
            _spriteComponent.SetAtlas("player sprite");
            Origin = new Vector2(12, 22);
        }
    }
}
