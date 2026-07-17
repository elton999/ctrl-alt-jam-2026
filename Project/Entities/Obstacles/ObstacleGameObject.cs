using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Physics;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;
using Project.Components;
using System.Collections;

namespace Project.Entities.Obstacles
{
    public abstract class ObstacleGameObject : GameObject
    {
        protected SpriteComponent _spriteComponent;
        protected ActorComponent _actorComponent;
        protected SpriteComponent _toolBalloonSprite;

        private UIAnimationComponent _balloonAnimation;

        public abstract ObstaclesTypes ObstacleType { get; }
        public abstract ToolsTypes ToolType { get; }

        public override void Start()
        {
            _spriteComponent = AddComponent<SpriteComponent>();
            _spriteComponent.SetAtlas("debug sprite");
            _actorComponent = AddComponent<ActorComponent>();
            _actorComponent.Size = new Point(23, 23);
            _actorComponent.HasGravity = false;

            var balloon = new GameObject();
            Scene.AddGameObject(balloon, Layers.FOREGROUND);
            balloon.Position = Position;
            balloon.Position -= Vector2.UnitY * 22f;

            _toolBalloonSprite = balloon.AddComponent<SpriteComponent>();
            _balloonAnimation = balloon.AddComponent<UIAnimationComponent>();
            _balloonAnimation.EndScale = 0.001f;
            _balloonAnimation.StartScale = 2f;
            _balloonAnimation.TweenType = Tweening.TweenType.ElasticEaseIn;
            _balloonAnimation.CalculateOrigin = false;

            ChosenToolsSubmitComponent.OnSubmitChosenTools += OnStartLevelFirstTime;
        }

        public override void OnDestroy()
        {
            ChosenToolsSubmitComponent.OnSubmitChosenTools -= OnStartLevelFirstTime;
        }

        public virtual bool PassObstacle()
        {
            if (InventoryGameObject.UseItem(ToolType))
            {
                Destroy();
                return false;
            }

            return false;
        }

        private void OnStartLevelFirstTime(ToolsTypes[] tools) => CoroutineManagement.StarCoroutine(StartBalloonAnimation());

        private IEnumerator StartBalloonAnimation()
        {
            yield return CoroutineManagement.Wait(2f);

            _toolBalloonSprite.OrigenToCenter();
            _toolBalloonSprite.GameObject.Position += _toolBalloonSprite.Origin;

            _balloonAnimation.StartAnimation();

            yield return null;
        }
    }
}
