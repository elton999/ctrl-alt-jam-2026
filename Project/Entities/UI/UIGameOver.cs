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

            yield return CoroutineManagement.Wait(0.8f);

            var background = new GameObject();
            Scene.AddGameObject(background, Layers.UI);
            var backgroundSprite = background.AddComponent<UIBackgroundSpriteComponent>();
            backgroundSprite.SetSprite(SquareSprite.SquareTexture, _backgroundColor);
            background.Transparent = 0.0f;

            var skull = new GameObject();
            Scene.AddGameObject(skull, Layers.UI);
            skull.tag = "skull sprite";
            var spriteComponent = skull.AddComponent<SpriteComponent>();
            spriteComponent.SetAtlas("skull");
            skull.Position = Scene.Sizes.ToVector2().Half().Truncate();
            skull.Position -= spriteComponent.Sprite.Body.Size.ToVector2().Half().Truncate();
            skull.Position += Vector2.UnitY * -30f;

            var skullAnimation = skull.AddComponent<UIAnimationComponent>();
            skullAnimation.AnimationDuration = 1f;
            skullAnimation.StartScale = 0.05f;
            skullAnimation.EndScale = 50.0f;
            skullAnimation.TweenType = Tweening.TweenType.EaseInQuad;
            skullAnimation.CalculateOrigin = true;
            skullAnimation.StartAnimation();

            yield return CoroutineManagement.Wait(1f);

            skullAnimation.AnimationDuration = 0.8f;
            skullAnimation.StartScale = 50.0f;
            skullAnimation.EndScale = 1.0f;
            skullAnimation.TweenType = Tweening.TweenType.EaseOutQuad;
            skullAnimation.StartAnimation();

            background.Transparent = 1f;

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
