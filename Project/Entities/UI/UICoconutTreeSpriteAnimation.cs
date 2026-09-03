using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine.GameSettings;
using Microsoft.Xna.Framework;
using System.Collections;
using System;

namespace Project.Entities.UI
{
    public class UICoconutTreeSpriteAnimation : GameObject
    {
        [ShowEditor] private Sprite _sprite;

        public override void Start()
        {
            Tag = nameof(UICoconutTreeSpriteAnimation);
            var atlas = GameSettingsProperty.GetProperty<AtlasGameSettings>(@"Content/AtlasGameSettings");
            if (atlas.TryGetSpriteByName("coqueiro", out var spriteAtlas))
            {
                _sprite = new Sprite(spriteAtlas.Name, spriteAtlas.Path, spriteAtlas.GetRectangle());
                _sprite.SetContentManager(Scene.Content);
                Sprite = _sprite.Texture;
                Body = _sprite.Body;

                Origin = new Vector2(((float)Body.Width).Half(), Body.Height);
                return;
            }
        }

        public void StartAnimation()
        {
            CoroutineManagement.StarCoroutine(SpawnAnimation());
        }

        private IEnumerator SpawnAnimation()
        {
            var initialPosition = Position + Vector2.UnitY * 10f;
            Position += Vector2.UnitY * Body.Height;
            var hidePosition = Position;
            float animationDuration = 2f;
            float animationTimer = 0f;

            while (initialPosition.Y <= Position.Y)
            {
                Position = new Vector2(Position.X, Tweening.BackEaseOut(hidePosition.Y, -Body.Height, animationTimer, animationDuration));
                animationTimer += (float)CoroutineManagement.GameTime.ElapsedGameTime.TotalSeconds;
                yield return null;
            }

            Position = initialPosition;

            yield return IdleAnimation();
        }

        private IEnumerator IdleAnimation()
        {
            var initialPosition = Position;

            while (true)
            {
                float deltaTime = (float)CoroutineManagement.GameTime.TotalGameTime.TotalSeconds;

                Position = (new Vector2(initialPosition.X + MathF.Cos(deltaTime * 0.2f) * 5f, initialPosition.Y + MathF.Sin(deltaTime * 0.2f) * 3f)).ToPoint().ToVector2();
                Rotation = (MathF.Cos(deltaTime) * 0.2f) * 20f * MathUtils.DegreesToRadians;
                yield return null;
            }

        }
    }
}
