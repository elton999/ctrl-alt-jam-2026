using Project.Entities.UI;

namespace Project.Entities.Obstacles
{
    public class Tree : ObstacleGameObject
    {
        public override ObstaclesTypes ObstacleType => ObstaclesTypes.TREE;
        public override ToolsTypes ToolType => ToolsTypes.AXE;

        public override void Start()
        {
            base.Start();
            _spriteComponent.SetAtlas("tree");
        }

        public override bool PassObstacle()
        {
            var pos = Position;
            var tool = ToolType;

            if (InventoryGameObject.HasItem(tool))
            {
                UIUseToolEfx.Instance.Play(tool, pos);
            }
            else
            {
                UIUseToolEfx.Instance.PlayMiss(pos);
            }
            return base.PassObstacle();
        }
    }
}
