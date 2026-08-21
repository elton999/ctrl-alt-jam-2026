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
            None,
            LEFT,
            RIGHT
        }

        [ShowEditor] private RenderPosition _renderPosition;
        private SpriteComponent _spriteComponent;
        [ShowEditor] private bool _isAnimating = true;
        [ShowEditor] private float _animationTimer;
        [ShowEditor] private float _animationDuration = 0.7f;
        [ShowEditor] private Vector2 _offset = Vector2.Zero;
        private float _animationFactor = 1f;

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
        }

        public override void Update(float deltaTime)
        {
            if (!_isAnimating) return;

            _animationTimer += deltaTime * _animationFactor;

            GameObject.Position = new Vector2(GameObject.Position.X, Tweening.BounceEaseOutSoft(GetHidePosition().Y, -_spriteComponent.Sprite.Size.Y, _animationTimer, _animationDuration));

            if (_animationTimer >= _animationDuration && _animationFactor > 0.0f)
            {
                _isAnimating = false;
                GameObject.Position = GetInitialPosition();
            }

            if (_animationTimer <= 0.0f && _animationFactor < 0.0f)
            {
                _isAnimating = false;
            }
        }

        public void SetHidePosition()
        {
            GameObject.Position = GetHidePosition();
        }

        public void SetAnimationDuration(float animationDuration)
        {
            _animationDuration = animationDuration;
        }

        public void SetRenderPosition(RenderPosition renderPosition)
        {
            _renderPosition = renderPosition;
        }

        public void SetOffset(Vector2 offset)
        {
            _offset = offset;
        }

        public Vector2 GetHidePosition()
        {
            _isAnimating = true;

            var position = GetInitialPosition();
            position += Vector2.UnitY * _spriteComponent.Sprite.Size.Y;
            return position;
        }

        public void SetReverseAnimation()
        {
            _animationFactor = -1f;
            _isAnimating = true;
        }

        public Vector2 GetInitialPosition()
        {
            float positionY = GameObject.Scene.Sizes.Y - _spriteComponent.Sprite.Size.Y;
            float positionX = GameObject.Position.X;

            if (_renderPosition == RenderPosition.LEFT)
            {
                positionX = 0f;
            }

            if (_renderPosition == RenderPosition.RIGHT)
            {
                positionX = GameObject.Scene.Sizes.X - _spriteComponent.Sprite.Size.X;
            }

            return new Vector2(positionX + _offset.X, positionY + _offset.Y);
        }

    }
}
