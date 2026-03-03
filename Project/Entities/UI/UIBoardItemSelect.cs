using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;

namespace Project.Entities.UI
{
    public class UIBoardItemSelect : GameObject
    {
        public override void Start()
        {
            tag = "board item select";
            var sprite = AddComponent<SpriteComponent>();
            sprite.SetAtlas("board item select");
            sprite.Origin = sprite.Sprite.Size.Half();

            Position = Scene.Sizes.ToVector2().Half();

            var initialAnimation = AddComponent<UIAnimationByPlayerMovementComponent>();
            initialAnimation.TweenType = Tweening.TweenType.ElasticEaseOut;
            initialAnimation.MaxScale = 0.0001f;
            initialAnimation.DefaulfScale = 1.0f;
            initialAnimation.AnimationDuration = 1.5f;
            initialAnimation.CalculateOrigin = false;
            Scale = initialAnimation.MaxScale;
            initialAnimation.StartAnimation();
        }
    }
}
