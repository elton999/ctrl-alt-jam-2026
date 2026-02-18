using Microsoft.Xna.Framework;

namespace Project.Entities.Obstacles
{
    public class Barrel : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.BARREL;
        public override ToolsTypes ToolType => ToolsTypes.BOOT;

        public override void Start()
        {
            SpriteColor = Color.Brown;
            base.Start();
        }

        public override bool PassObstacle()
        {
            if (InventoryGameObject.UseItem(ToolType))
            {
                var playerPosition = Scene.Players[0].Position;
                var direction = Position - playerPosition;
                direction.Normalize();

                var tempPosition = direction * Scene.CellSize + Position;
                var nextTile = (tempPosition / Scene.CellSize).ToPoint();

                if (nextTile.Y < Scene.Grid.GridCollides.Count && nextTile.X < Scene.Grid.GridCollides[nextTile.Y].Count)
                {
                    if (Scene.Grid.GridCollides[nextTile.Y][nextTile.X] == "2")
                    {
                        Position = tempPosition;
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
    }
}
