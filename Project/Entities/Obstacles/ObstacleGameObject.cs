using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Physics;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Entities.Obstacles
{
    public abstract class ObstacleGameObject : GameObject
    {
        private SpriteComponent _spriteComponent;
        private ActorComponent _actorComponent;

        public abstract ObstaclesTypes ObstacleType { get; }
        public abstract ToolsTypes ToolType { get; }

        public override void Start()
        {
            _spriteComponent = AddComponent<SpriteComponent>();
            _spriteComponent.SetAtlas("debug sprite");
            _actorComponent = AddComponent<ActorComponent>();
            _actorComponent.Size = new Point(23, 23);
            _actorComponent.HasGravity = false;
        }

        public virtual bool PassObstacle()
        {
            if (InventoryGameObject.UseItem(ToolType))
            {
                Destroy();
                return true;
            }

            return false;
        }
    }
}
