using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Entities.Obstacles
{
    public class Enemy : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.ENEMY;
        public override ToolsTypes ToolType => ToolsTypes.SWORD;
        private AnimationComponent _animation;

        public override void Start()
        {
            base.Start();
            _animation = AddComponent<AnimationComponent>();
            _animation.SetAnimationClip(new SpriteAnimationClip("enemy idle", 2, 0.3f, "idle"));
            _animation.Play("idle");

            _spriteComponent.Origin = new Vector2(15f, 21f);
        }

        public override bool PassObstacle()
        {
            Destroy();
            return false;
        }
    }
}
