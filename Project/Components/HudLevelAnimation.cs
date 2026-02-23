using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Components
{
    public class HudLevelAnimation : Component
    {
        public enum RenderPosition
        {
            LEFT,
            RIGHT
        }

        [ShowEditor] private RenderPosition _renderPosition;
        private SpriteComponent _spriteComponent;
        private bool _isAnimating = true;
        private float _animationTimer;
        private const float ANIMATION_DURATION = 0.7f;

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
            GameObject.Position = GetHidePosition();
        }

        public override void Update(float deltaTime)
        {
            if (!_isAnimating) return;

            _animationTimer += deltaTime;

            GameObject.Position = new Vector2(GameObject.Position.X, Tweening.EaseOutQuad(GetHidePosition().Y, -_spriteComponent.Sprite.Size.Y, _animationTimer, ANIMATION_DURATION));

            if (_animationTimer >= ANIMATION_DURATION)
            {
                _isAnimating = false;
                GameObject.Position = GetInitialPosition();
            }
        }

        public void SetRenderPosition(RenderPosition renderPosition)
        {
            _renderPosition = renderPosition;
            GameObject.Position = GetHidePosition();
        }

        public Vector2 GetHidePosition()
        {
            _isAnimating = true;

            var position = GetInitialPosition();
            position += Vector2.UnitY * _spriteComponent.Sprite.Size.Y;
            return position;
        }

        public Vector2 GetInitialPosition()
        {
            float positionY = GameObject.Scene.Sizes.Y - _spriteComponent.Sprite.Size.Y;
            float positionX = 0;

            if (_renderPosition == RenderPosition.RIGHT)
            {
                positionX = GameObject.Scene.Sizes.X - _spriteComponent.Sprite.Size.X;
            }

            return new Vector2(positionX, positionY);
        }
        
    }
}
