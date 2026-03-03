using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine;

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
                    (_spriteComponent.Sprite.Size.X * MaxScale - _spriteComponent.Sprite.Size.X) / 2.0f,
                    (_spriteComponent.Sprite.Size.Y * MaxScale - _spriteComponent.Sprite.Size.Y) / 2.0f
                );
            }
        }
        [ShowEditor] public float AnimationDuration = 0.3f;
        [ShowEditor] public float MaxScale = 1.1f;
        [ShowEditor] public float DefaulfScale = 1.0f;
        [ShowEditor] public Tweening.TweenType TweenType = Tweening.TweenType.BounceEaseOut;
        [ShowEditor] public bool CalculateOrigin = true;

        private SpriteComponent _spriteComponent;

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
            if (_spriteComponent == null)
            {
                    Log.Write($"[{nameof(UIAnimationByPlayerMovementComponent)}] SpriteComponent not found on GameObject: " + GameObject.tag);
                    return;
            }
        }

        public override void Update(float deltaTime)
        {
            if (animationTimer >= AnimationDuration) return;

            animationTimer += deltaTime;

            GameObject.Scale = Tweening.GetTweeningValue(TweenType, MaxScale, -(MaxScale - DefaulfScale), animationTimer, AnimationDuration);

            if (CalculateOrigin)
            {
                _spriteComponent.Origin = new Vector2
               (
                   Tweening.GetTweeningValue(TweenType, _spriteOrigin.X, -_spriteOrigin.X, animationTimer, AnimationDuration),
                   Tweening.GetTweeningValue(TweenType, _spriteOrigin.Y, -_spriteOrigin.Y, animationTimer, AnimationDuration)
               );
            }
           
            if (animationTimer >= AnimationDuration)
            {
                GameObject.Scale = DefaulfScale;
                if (CalculateOrigin) _spriteComponent.Origin = Vector2.Zero;
            }
        }

        [Button]
        public void StartAnimation()
        {
            GameObject.Scale =  MaxScale;
            animationTimer = 0;
            if (CalculateOrigin) _spriteComponent.Origin = _spriteOrigin;
        }
    }
}
