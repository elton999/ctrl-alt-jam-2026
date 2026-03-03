using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities.UI
{
    public class UIBoardItemSelect : GameObject
    {
        public override void Start()
        {
            tag = "board item select";
            AddComponent<SpriteComponent>().SetAtlas("board item select");
            var initialAnimation = AddComponent<UIAnimationByPlayerMovementComponent>();
            initialAnimation.TweenType = Tweening.TweenType.ElasticEaseOut;
            initialAnimation.MaxScale = 0.0001f;
            initialAnimation.DefaulfScale = 1.0f;
            initialAnimation.AnimationDuration = 1.5f;
            Scale = initialAnimation.MaxScale;
            initialAnimation.StartAnimation();
        }
    }
}
