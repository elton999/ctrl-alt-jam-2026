using System.Collections.Generic;

namespace UmbrellaToolsKit.Components.Sprite
{
    public class AnimationComponent : Component
    {
        private List<SpriteAnimationClip> _animations = new();
        private int _currentFrame;
        private float _currentTime;
        private SpriteAnimationClip _currentAnimation;
        private SpriteComponent _spriteComponent;

        public override void Start()
        {
            _spriteComponent = GameObject.GetComponent<SpriteComponent>();
            if (_spriteComponent == null)
            {
                _spriteComponent = GameObject.AddComponent<SpriteComponent>();
            }
        }

        public override void Update(float deltaTime)
        {
            if (_currentAnimation is null) return;
            _currentTime += deltaTime;

            var frame = _currentAnimation.Frames[_currentFrame];
            if (frame.Duration <= _currentTime)
            {
                _currentFrame++;
                if (_currentFrame == _currentAnimation.Frames.Count)
                {
                    _currentFrame = 0;
                }
                frame = _currentAnimation.Frames[_currentFrame];
            }

            _spriteComponent.SetSprite(frame.Sprite);
        }

        public void SetAnimationClip(SpriteAnimationClip animationClip) => _animations.Add(animationClip);

        public void Play(SpriteAnimationClip animationClip)
        {
            if (!_animations.Contains(animationClip))
            {
                SetAnimationClip(animationClip);
            }

            _currentAnimation = animationClip;

            Restart();
        }

        public void Play(string animation)
        {
            foreach (var animationClip in _animations)
            {
                if (animationClip.ClipName == animation)
                {
                    Play(animationClip);
                    return;
                }
            }
        }

        public void Restart()
        {
            _currentFrame = 0;
            _currentTime = 0.0f;
        }
    }
}
