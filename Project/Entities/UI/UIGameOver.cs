using System.Collections;
using Microsoft.Xna.Framework;
using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine;

namespace Project.Entities.UI
{
    public class UIGameOver : GameObject
    {
        private Color _backgroundColor = (new Vector3(64f, 73f, 115f)).ToColor();

        public override void Start()
        {
            tag = "game over screen";
            LevelManagerEntity.OnLevelStateChanged += OnChangeLevelState;
        }

        public override void Destroy()
        {
            LevelManagerEntity.OnLevelStateChanged -= OnChangeLevelState;
        }

        private void OnChangeLevelState(LevelManagerEntity.GameState state)
        {
            if (state != LevelManagerEntity.GameState.GAME_OVER)
                return;

            CoroutineManagement.StarCoroutine(ShowScreen());
        }

        private IEnumerator ShowScreen()
        {
            Log.Write("Show game over screen");
            yield return CoroutineManagement.Wait(2.0f);

            var background = new GameObject();
            Scene.AddGameObject(background, Layers.UI);
            background
                .AddComponent<UIBackgroundSpriteComponent>()
                .SetSprite(SquareSprite.SquareTexture, _backgroundColor);
            yield return null;
        }
    }
}
