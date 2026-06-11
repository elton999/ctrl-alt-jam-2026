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

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
            GameObject.Position = GetHidePosition();
        }

        public override void Update(float deltaTime)
        {
            if (!_isAnimating) return;

            _animationTimer += deltaTime;

            GameObject.Position = new Vector2(GameObject.Position.X, Tweening.BounceEaseOutSoft(GetHidePosition().Y, -_spriteComponent.Sprite.Size.Y, _animationTimer, _animationDuration));

            if (_animationTimer >= _animationDuration)
            {
                _isAnimating = false;
                GameObject.Position = GetInitialPosition();
            }
        }

        public void SetAnimationDuration(float animationDuration)
        {
            _animationDuration = animationDuration;
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
