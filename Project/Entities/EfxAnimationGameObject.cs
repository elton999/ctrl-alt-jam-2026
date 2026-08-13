using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Entities
{
    public class EfxAnimationGameObject : GameObject
    {
        public enum AnimationType
        {
            HIT = 0,
            EXPLOSION = 1,
            SKULL = 2
        }

        private List<SpriteAnimationClip> _animationClips;
        private List<Vector2> _animationOffset = new() { new Vector2(11f, 11f), Vector2.Zero, Vector2.Zero };
        private AnimationComponent _animationComponent;

        public override void Start()
        {
            _animationClips = new List<SpriteAnimationClip>()
            {
                new SpriteAnimationClip("explosion animation", 5, 0.3f),
                new SpriteAnimationClip("explosion animation", 5, 0.3f),
                new SpriteAnimationClip("death skull", 11, 0.3f),
            };
        }

        public void SetAnimation(AnimationType animationType, Vector2 position)
        {
            int animationIndex = (int)animationType;
            _animationComponent = AddComponent<AnimationComponent>();
            _animationComponent.SetAnimationClip(_animationClips[animationIndex]);
            Position = position - _animationOffset[animationIndex];
        }
    }
}
