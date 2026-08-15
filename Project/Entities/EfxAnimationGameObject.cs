using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.Interfaces;
using UmbrellaToolsKit.Utils;

namespace Project.Entities
{
    public class EfxAnimationGameObject : GameObject, IPoolable
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
                new SpriteAnimationClip("death skull ", 11, 0.3f),
            };

            _animationComponent = AddComponent<AnimationComponent>();
        }

        public void SetAnimation(AnimationType animationType, Vector2 position)
        {
            int animationIndex = (int)animationType;
            _animationComponent.Play(_animationClips[animationIndex]);
            Position = position - _animationOffset[animationIndex];
        }

        public void Reset() { }
    }

    public class HitfxAnimation : GameObject
    {
        private static HitfxAnimation _instance = null;

        public ObjectPooling<EfxAnimationGameObject> EfxObjectPooling;
        public static HitfxAnimation Instance => _instance;

        public override void Start()
        {
            EfxObjectPooling = new ObjectPooling<EfxAnimationGameObject>(3);
            if (_instance == null)
                _instance = this;
        }

        public static void Play(EfxAnimationGameObject.AnimationType animationType, Vector2 position)
        {
            var efxObject = _instance.EfxObjectPooling.GetObject();

            if (efxObject is EfxAnimationGameObject)
            {
                var efx = ((EfxAnimationGameObject)efxObject);
                if (efx.Scene == null)
                    _instance.Scene.AddGameObject(efx, Layers.FOREGROUND);

                efx.SetAnimation(animationType, position);
            }
        }
    }
}
