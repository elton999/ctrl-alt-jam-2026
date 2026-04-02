namespace Project.Entities.Obstacles
{
    public class EndTile : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.END;
        public override ToolsTypes ToolType => ToolsTypes.NONE;

        public override void Start()
        {
            base.Start();
            _spriteComponent.SetAtlas("end sprite");
        }

        public override bool PassObstacle()
        {
            return true;
        }
    }
}
