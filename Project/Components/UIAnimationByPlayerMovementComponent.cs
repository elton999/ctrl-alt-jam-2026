using Microsoft.Xna.Framework;
using Project.Entities;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Components
{
    public class UIAnimationByPlayerMovementComponent : Component
    {
        [ShowEditor] private float animationTimer;
        private Vector2 _spriteOrigin
        {
            get
            {
                return new Vector2
                (
                    (_spriteComponent.Sprite.Size.X * MAX_SCALE - _spriteComponent.Sprite.Size.X) / 2.0f,
                    (_spriteComponent.Sprite.Size.Y * MAX_SCALE - _spriteComponent.Sprite.Size.Y) / 2.0f
                );
            }
        }
        private const float ANIMATION_DURATION = 0.3f;
        private const float MAX_SCALE = 1.1f;
        private const float DEFAULT_SCALE = 1.0f;

        private SpriteComponent _spriteComponent;

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
            Player.OnPlayerMove += StartAnimation;
        }

        public override void OnDestroy()
        {
            Player.OnPlayerMove -= StartAnimation;
        }

        public override void Update(float deltaTime)
        {
            if (animationTimer >= ANIMATION_DURATION) return;

            animationTimer += deltaTime;

            GameObject.Scale = Tweening.EaseOutQuad(MAX_SCALE, -(MAX_SCALE - DEFAULT_SCALE), animationTimer, ANIMATION_DURATION);
            _spriteComponent.Origin = new Vector2
            (
                Tweening.EaseOutQuad(_spriteOrigin.X, -_spriteOrigin.X, animationTimer, ANIMATION_DURATION),
                Tweening.EaseOutQuad(_spriteOrigin.Y, -_spriteOrigin.Y, animationTimer, ANIMATION_DURATION)
            );

            if (animationTimer >= ANIMATION_DURATION)
            {
                GameObject.Scale = DEFAULT_SCALE;
                _spriteComponent.Origin = Vector2.Zero;
            }
        }

        [Button]
        public void StartAnimation()
        {
            GameObject.Scale =  MAX_SCALE;
            animationTimer = 0;
            _spriteComponent.Origin = _spriteOrigin;
        }
    }
}
