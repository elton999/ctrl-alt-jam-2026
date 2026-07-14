using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine;

namespace Project.Components
{
    public class UIAnimationComponent : Component
    {
        [ShowEditor] private float animationTimer = float.MaxValue;
        private Vector2 _spriteOrigin
        {
            get
            {
                return (_spriteComponent.Sprite.Size * GameObject.Scale - _spriteComponent.Sprite.Size).Half() * -1.0f;
            }
        }
        private SpriteComponent _spriteComponent;
        private Vector2 _startPosition;

        [ShowEditor] public float AnimationDuration = 0.3f;
        [ShowEditor] public float EndScale = 1.1f;
        [ShowEditor] public float StartScale = 1.0f;
        [ShowEditor] public Tweening.TweenType TweenType = Tweening.TweenType.BounceEaseOut;
        [ShowEditor] public bool CalculateOrigin = true;
        [ShowEditor] public bool ResetScaleOnStop = false;
        public SpriteComponent SpriteComponent => _spriteComponent;

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
            if (_spriteComponent == null)
            {
                Log.Write($"[{nameof(UIAnimationComponent)}] SpriteComponent not found on GameObject: " + GameObject.tag);
                return;
            }
        }

        public override void Update(float deltaTime)
        {
            if (animationTimer >= AnimationDuration) return;

            animationTimer += deltaTime;

            if (StartScale > EndScale)
            {
                GameObject.Scale = Tweening.GetTweeningValue(TweenType, StartScale, -(StartScale - EndScale), animationTimer, AnimationDuration);
            }
            else
            {
                GameObject.Scale = Tweening.GetTweeningValue(TweenType, StartScale, EndScale - StartScale, animationTimer, AnimationDuration);
            }

            if (CalculateOrigin)
            {
                GameObject.Position = _startPosition + _spriteOrigin;
            }

            if (animationTimer >= AnimationDuration)
            {
                GameObject.Scale = ResetScaleOnStop ? StartScale : EndScale;
                if (CalculateOrigin) GameObject.Position = _startPosition;
            }
        }

        [Button]
        public void StartAnimation()
        {
            if (animationTimer >= AnimationDuration)
                _startPosition = GameObject.Position;
            GameObject.Scale = StartScale;
            animationTimer = 0.0f;
            Update(0.0f);
        }
    }
}
