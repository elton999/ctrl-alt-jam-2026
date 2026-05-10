using Project.Entities.UI;

namespace Project.Entities.Obstacles
{
    public class Stone : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.STONE;
        public override ToolsTypes ToolType => ToolsTypes.BOMB;

        public override void Start()
        {
            base.Start();
            _spriteComponent.SetAtlas("stone");
        }

        public override bool PassObstacle()
        {
            var pos = Position;
            var tool = ToolType;

            if (InventoryGameObject.HasItem(tool))
            {
                UIUseToolEfx.Instance.Play(tool, pos);
            }
            return base.PassObstacle();
        }
    }
}
