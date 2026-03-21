using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using Microsoft.Xna.Framework;

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
            sprite.UpdateSprite();

            Position = Scene.Sizes.ToVector2().Half();

            var initialAnimation = AddComponent<UIAnimationComponent>();
            initialAnimation.TweenType = Tweening.TweenType.BackEaseOut;
            initialAnimation.MaxScale = 0.0001f;
            initialAnimation.DefaulfScale = 1.0f;
            initialAnimation.AnimationDuration = 0.3f;
            initialAnimation.CalculateOrigin = false;

            Scale = initialAnimation.MaxScale;
            initialAnimation.StartAnimation();

            _buttonSelect1 = new GameObject();
            Scene.AddGameObject(_buttonSelect1, Layers.UI);
            var buttonSprite1 = _buttonSelect1.AddComponent<SpriteComponent>();
            buttonSprite1.SetAtlas("ui axe");
            _buttonSelect1.AddComponent<UIItemToolButtonComponent>().SetTool(ToolsTypes.AXE);

            _buttonSelect2 = new GameObject();
            Scene.AddGameObject(_buttonSelect2, Layers.UI);
            var buttonSprite2 = _buttonSelect2.AddComponent<SpriteComponent>();
            buttonSprite2.SetAtlas("ui bomb");
            _buttonSelect2.AddComponent<UIItemToolButtonComponent>().SetTool(ToolsTypes.BOMB);

            _buttonSelect3 = new GameObject();
            Scene.AddGameObject(_buttonSelect3, Layers.UI);
            var buttonSprite3 = _buttonSelect3.AddComponent<SpriteComponent>();
            buttonSprite3.SetAtlas("ui boots");
            _buttonSelect3.AddComponent<UIItemToolButtonComponent>().SetTool(ToolsTypes.BOOT);

            _buttonConfirm = new GameObject();
            Scene.AddGameObject(_buttonConfirm, Layers.UI);
            _buttonConfirm.AddComponent<ChosenToolsSubmitComponent>();

            var spriteGrid = AddComponent<UISpriteGridComponent>();
            spriteGrid.SetSize(sprite.Sprite.Size);
            spriteGrid.SetSpacing(new Vector2(5f, Origin.Y));
            spriteGrid.AddSprite(buttonSprite1);
            spriteGrid.AddSprite(buttonSprite2);
            spriteGrid.AddSprite(buttonSprite3);

            buttonSprite1.OrigenToCenterKeepPosition();
            buttonSprite2.OrigenToCenterKeepPosition();
            buttonSprite3.OrigenToCenterKeepPosition();

            _buttonSelect1.AddComponent<UIButtonToolSelectAnimationComponent>().DelayToStartAnimation = 0.3f;
            _buttonSelect2.AddComponent<UIButtonToolSelectAnimationComponent>().DelayToStartAnimation = 0.6f;
            _buttonSelect3.AddComponent<UIButtonToolSelectAnimationComponent>().DelayToStartAnimation = 0.9f;


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
