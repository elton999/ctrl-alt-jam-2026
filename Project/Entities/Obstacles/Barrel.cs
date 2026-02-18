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
    }
}
