using Microsoft.Xna.Framework;

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
    }
}
