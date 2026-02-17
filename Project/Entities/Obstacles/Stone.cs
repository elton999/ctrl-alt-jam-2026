using Microsoft.Xna.Framework;

namespace Project.Entities.Obstacles
{
    public class Stone : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.STONE;

        public override void Start()
        {
            SpriteColor = Color.Gray;
            base.Start();
        }
    }
}
