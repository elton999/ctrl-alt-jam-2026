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
    }
}
