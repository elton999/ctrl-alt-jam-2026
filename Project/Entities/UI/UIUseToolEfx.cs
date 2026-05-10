using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.Utils;
using UmbrellaToolsKit.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Project.Entities.UI
{
    public class UIToolEfx : GameObject, IPoolable
    {
        private float _animationDuration = 0.5f;
        private float _maxDistance = 22f;
        private SpriteComponent _spriteComponent;

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

        private IEnumerator EfxAnimationCourotine()
        {
            float timer = 0f;
            float yValue = Position.Y;


            while (timer < _animationDuration)
            {
                timer += (float)CoroutineManagement.GameTime.ElapsedGameTime.TotalSeconds;
                float y = Tweening.GetTweeningValue(Tweening.TweenType.BackEaseOut, yValue, -_maxDistance, timer, _animationDuration);
                Position = new Vector2(Position.X, y);
                Position.Truncate();
                _spriteComponent.Transparent = 1f;
                yield return null;
            }

            _spriteComponent.Transparent = 0f;

            yield break;
        }
    }

    public class UIUseToolEfx : GameObject
    {
        private const int POOLING_COUNT = 4;
        public ObjectPooling<UIToolEfx> EfxPooling;

        public static UIUseToolEfx Instance;

        public override void Start()
        {
            EfxPooling = new ObjectPooling<UIToolEfx>(POOLING_COUNT);

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
    }
}
