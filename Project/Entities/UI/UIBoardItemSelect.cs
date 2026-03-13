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
            var sprite = AddComponent<SpriteComponent>();
            sprite.SetAtlas("board item select");
            sprite.Origin = sprite.Sprite.Size.Half();

            Position = Scene.Sizes.ToVector2().Half();

            var initialAnimation = AddComponent<UIAnimationByPlayerMovementComponent>();
            initialAnimation.TweenType = Tweening.TweenType.BackEaseOut;
            initialAnimation.MaxScale = 0.0001f;
            initialAnimation.DefaulfScale = 1.0f;
            initialAnimation.AnimationDuration = 0.3f;
            initialAnimation.CalculateOrigin = false;

            Scale = initialAnimation.MaxScale;
            initialAnimation.StartAnimation();

            var buttonSelect1 = new GameObject();
            Scene.AddGameObject(buttonSelect1, Layers.UI);
            buttonSelect1.AddComponent<UIItemToolButtonComponent>();

            var buttonSelect2 = new GameObject();
            Scene.AddGameObject(buttonSelect2, Layers.UI);
            buttonSelect2.AddComponent<UIItemToolButtonComponent>();

            var buttonSelect3 = new GameObject();
            Scene.AddGameObject(buttonSelect3, Layers.UI);
            buttonSelect3.AddComponent<UIItemToolButtonComponent>();
        }
    }
}
