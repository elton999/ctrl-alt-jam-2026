using Microsoft.Xna.Framework;
using Project.Components;
using UmbrellaToolsKit;

namespace Project.Entities.Obstacles
{
    public class Barrel : ObstacleGameObject
    {
        private UIAnimationComponent _animationComponent;

        public override ObstaclesTypes ObstacleType => ObstaclesTypes.BARREL;
        public override ToolsTypes ToolType => ToolsTypes.BOOT;

        public override void Start()
        {
            base.Start();

            _spriteComponent.SetAtlas("barrel");
            _animationComponent = AddComponent<UIAnimationComponent>();
            _animationComponent.MaxScale = 1.5f;
            _animationComponent.AnimationDuration = 0.7f;
            _animationComponent.TweenType = Tweening.TweenType.ElasticEaseIn;
        }

        public override bool PassObstacle()
        {
            if (InventoryGameObject.UseItem(ToolType))
            {
                var playerPosition = Scene.Players[0].Position;
                var direction = Position - playerPosition;
                direction.Normalize();

                var tempPosition = direction * Scene.CellSize + Position;
                var nextTile = (tempPosition / Scene.CellSize + Vector2.One).ToPoint();

                if (nextTile.Y < Scene.Grid.GridCollides.Count && nextTile.X < Scene.Grid.GridCollides[nextTile.Y].Count)
                {

                    if (Scene.Grid.GridCollides[nextTile.Y][nextTile.X] == "2")
                    {
                        Position = tempPosition;
                        _animationComponent.StartAnimation();
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
    }
}
