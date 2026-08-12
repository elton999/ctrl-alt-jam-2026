using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities
{
    public class EfxAnimationGameObject : GameObject
    {
        public enum AnimationType
        {
            HIT = 0,
            SKULL = 1
        }

        private List<SpriteAnimationClip> _animationClips;
        private AnimationComponent _animationComponent;

        public override void Start()
        {
            _animationClips = new List<SpriteAnimationClip>();
            _animationComponent = AddComponent<AnimationComponent>();
        }

        public void SetAnimation(AnimationType animationType)
        {

        }
    }
}
