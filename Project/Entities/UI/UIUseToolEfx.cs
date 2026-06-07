using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.Utils;
using UmbrellaToolsKit.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;
using System;

namespace Project.Entities.UI
{
    public class UIToolEfx : GameObject, IPoolable
    {
        private float _animationDuration = 0.5f;
        private float _maxDistance = 22f;
        private SpriteComponent _spriteComponent;

        public SpriteComponent SpriteComponent => _spriteComponent ??= GetComponent<SpriteComponent>();

        public void Reset()
        {
            Transparent = 1f;
        }

        public void SetSprite(ToolsTypes tool)
        {
            _spriteComponent ??= GetComponent<SpriteComponent>();

            switch (tool)
            {
                case ToolsTypes.AXE:
                    _spriteComponent.SetAtlas("axe use efx");
                    break;
                case ToolsTypes.BOOT:
                    _spriteComponent.SetAtlas("boot use efx");
                    break;
                case ToolsTypes.BOMB:
                    _spriteComponent.SetAtlas("bomb use efx");
                    break;
                default:
                    return;
            }

            CoroutineManagement.StarCoroutine(EfxAnimationCourotine());
        }

        public void PlayAnimation()
        {
            CoroutineManagement.StarCoroutine(EfxAnimationCourotine());
        }

        private IEnumerator EfxAnimationCourotine()
        {
            float timer = 0f;
            float yValue = Position.Y;


            while (timer < _animationDuration)
            {
                timer += (float)CoroutineManagement.GameTime.ElapsedGameTime.TotalSeconds;
                float y = Tweening.GetTweeningValue(Tweening.TweenType.BackEaseInOut, yValue, -_maxDistance, timer, _animationDuration);
                Position = new Vector2(Position.X, y);
                Position.Truncate();
                SpriteComponent.Transparent = 1f;
                yield return null;
            }

            SpriteComponent.Transparent = 0f;

            yield break;
        }
    }

    public class UIUseToolEfx : GameObject
    {
        private const int POOLING_COUNT = 4;
        private Random _random = new Random();
        public ObjectPooling<UIToolEfx> EfxPooling;
        public ObjectPooling<UIToolEfx> EfxPoolingUI;

        public static UIUseToolEfx Instance;

        public override void Start()
        {
            EfxPooling = new ObjectPooling<UIToolEfx>(POOLING_COUNT);
            EfxPoolingUI = new ObjectPooling<UIToolEfx>(POOLING_COUNT);

            if (Instance == null)
            {
                Instance = this;
            }
        }

        public override void OnDestroy()
        {
            Instance = null;
        }

        public void Play(ToolsTypes tool, Vector2 pos)
        {
            var efx = (UIToolEfx)EfxPooling.GetObject();
            if (efx.Scene == null)
            {
                Scene.AddGameObject(efx, Layers.FOREGROUND);
                efx.AddComponent<SpriteComponent>();
            }

            efx.Position = pos;
            efx.SetSprite(tool);
            efx.Reset();
        }

        public void PlayMiss(Vector2 pos)
        {
            var efx = (UIToolEfx)EfxPooling.GetObject();
            if (efx.Scene == null)
            {
                Scene.AddGameObject(efx, Layers.FOREGROUND);
                efx.AddComponent<SpriteComponent>();
            }

            efx.SpriteComponent.SetAtlas("miss");

            efx.Position = pos;
            efx.PlayAnimation();
            efx.Reset();
            Scene.Camera.StartShake(3f);
        }

        public void PlayMovement(Vector2 pos)
        {
            var efx = (UIToolEfx)EfxPoolingUI.GetObject();
            if (efx.Scene == null)
            {
                Scene.AddGameObject(efx, Layers.UI);
                efx.AddComponent<SpriteComponent>();
            }

            efx.SpriteComponent.SetAtlas("movement use efx");

            efx.Position = pos + Vector2.UnitX * 50f;
            efx.Position += new Vector2(_random.Next(-20, 20), _random.Next(-10, 5));
            efx.PlayAnimation();
            efx.Reset();

        }
    }
}
