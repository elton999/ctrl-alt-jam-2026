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
        private Color _backgroundColor = (new Vector3(20f, 24f, 46f)).ToColor();

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

            yield return CoroutineManagement.Wait(0.1f);

            Scene.Camera.StartShake(10f, 10f);

            yield return CoroutineManagement.Wait(2.0f);

            var background = new GameObject();
            Scene.AddGameObject(background, Layers.UI);
            background.AddComponent<UIBackgroundSpriteComponent>().SetSprite(SquareSprite.SquareTexture, _backgroundColor);

            var skull = new GameObject();
            Scene.AddGameObject(skull, Layers.UI);
            var spriteComponent = skull.AddComponent<SpriteComponent>();
            spriteComponent.SetAtlas("skull");
            spriteComponent.OrigenToCenter();
            skull.Position = Scene.Sizes.ToVector2().Half().Truncate();
            skull.Position += Vector2.UnitY * -30f;

            var tryAgainButton = new GameObject();
            tryAgainButton.tag = "try again button";
            Scene.AddGameObject(tryAgainButton, Layers.UI);
            var tryAgainButtonSpriteComponent = tryAgainButton.AddComponent<SpriteComponent>();
            tryAgainButtonSpriteComponent.SetAtlas("try again btn");
            tryAgainButtonSpriteComponent.OrigenToCenter();
            tryAgainButton.Position = Scene.Sizes.ToVector2().Half();
            tryAgainButton.Position += Vector2.UnitY * 100.0f;

            yield return null;
        }
    }
}
