using Microsoft.Xna.Framework;
using Project.Components;
using UmbrellaToolsKit;
using Project.Entities.UI;

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
            _animationComponent.MaxScale = 1.3f;
            _animationComponent.AnimationDuration = 0.7f;
            _animationComponent.TweenType = Tweening.TweenType.ElasticEaseOut;
        }

        public override bool PassObstacle()
        {
            if (!InventoryGameObject.HasItem(ToolType))
            {
                UIUseToolEfx.Instance.PlayMiss(Position);
            }

            if (InventoryGameObject.UseItem(ToolType))
            {
                var playerPosition = Scene.Players[0].Position;
                var direction = Position - playerPosition;
                direction.Normalize();

                var tempPosition = direction * Scene.CellSize + Position;
                var nextTile = (tempPosition / Scene.CellSize + Vector2.One).ToPoint();



                if (nextTile.Y < Scene.Grid.GridCollides.Count && nextTile.X < Scene.Grid.GridCollides[nextTile.Y].Count)
                {
                    string freePath = "2";
                    if (Scene.Grid.GridCollides[nextTile.Y][nextTile.X] == freePath)
                    {
                        Position = tempPosition;
                        _animationComponent.StartAnimation();
                        UIUseToolEfx.Instance.Play(ToolType, Position);
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
    }
}
