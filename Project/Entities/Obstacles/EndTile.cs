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
            LevelManagerEntity.SetState(LevelManagerEntity.GameState.ENDING_LEVEL);
            yield return CoroutineManagement.Wait(2.0f);
            int currentSceneIndex = Scene.SceneManagement.CurrentScene + 1;
            Scene.GameManagement.SceneManagement.SetScene(currentSceneIndex);
            yield return null;
        }
    }
}
