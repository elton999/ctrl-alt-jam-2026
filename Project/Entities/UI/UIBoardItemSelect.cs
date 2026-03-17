using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities.UI
{
    public class UIBoardItemSelect : GameObject
    {
        private GameObject _buttonSelect1;
        private GameObject _buttonSelect2;
        private GameObject _buttonSelect3;
        private GameObject _buttonConfirm;

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

            _buttonSelect1 = new GameObject();
            Scene.AddGameObject(_buttonSelect1, Layers.UI);
            _buttonSelect1.AddComponent<UIItemToolButtonComponent>().SetTool(ToolsTypes.AXE);

            _buttonSelect2 = new GameObject();
            Scene.AddGameObject(_buttonSelect2, Layers.UI);
            _buttonSelect2.AddComponent<UIItemToolButtonComponent>().SetTool(ToolsTypes.BOMB);

            _buttonSelect3 = new GameObject();
            Scene.AddGameObject(_buttonSelect3, Layers.UI);
            _buttonSelect3.AddComponent<UIItemToolButtonComponent>().SetTool(ToolsTypes.BOOT);

            _buttonConfirm = new GameObject();
            Scene.AddGameObject(_buttonConfirm, Layers.UI);
            _buttonConfirm.AddComponent<ChosenToolsSubmitComponent>();

            ChosenToolsSubmitComponent.OnSubmitChosenTools += OnSubmit;
        }

        public override void OnDestroy()
        {
            ChosenToolsSubmitComponent.OnSubmitChosenTools -= OnSubmit;

            _buttonSelect1.Destroy();
            _buttonSelect2.Destroy();
            _buttonSelect3.Destroy();
            _buttonConfirm.Destroy();
        }

        public void OnSubmit(ToolsTypes[] chosenTools)
        {
            Destroy();
        }
    }
}
