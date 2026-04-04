using System.Collections;
using UmbrellaToolsKit;

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
            CoroutineManagement.StarCoroutine(EndAnimation());
            return true;
        }

        public IEnumerator EndAnimation()
        {
            yield return CoroutineManagement.Wait(5.0f);
            Scene.GameManagement.SceneManagement.SetScene(1);
            yield return null;
        }
    }
}
