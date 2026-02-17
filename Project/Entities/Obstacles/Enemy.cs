using Microsoft.Xna.Framework;

namespace Project.Entities.Obstacles
{
    public class Enemy : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.ENEMY;

        public override void Start()
        {
            SpriteColor = Color.Red;
            base.Start();
        }
    }
}
