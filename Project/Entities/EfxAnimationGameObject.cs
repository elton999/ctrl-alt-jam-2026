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
        private AnimationComponent _animationComponent;

        public override void Start()
        {
            _animationClips = new List<SpriteAnimationClip>()
            {
                (new SpriteAnimationClip("hit", 2, 0.05f)).AddFrame("empty frame", 0.05f),
                (new SpriteAnimationClip("explosion animation", 5, 0.05f)).AddFrame("empty frame", 0.05f),
                new SpriteAnimationClip("death skull", 11, 0.05f),
            };

            _animationComponent = AddComponent<AnimationComponent>();
        }

        public void SetAnimation(AnimationType animationType, Vector2 position)
        {
            int animationIndex = (int)animationType;
            Position = position + (new Vector2(-11, -11));
            _animationComponent.Play(_animationClips[animationIndex]);

            tag = "efx - " + animationType;
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

        public override void OnDestroy()
        {
            _instance = null;
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
