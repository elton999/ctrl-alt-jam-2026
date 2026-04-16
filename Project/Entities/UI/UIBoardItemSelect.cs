using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.Sprite;

namespace Project.Entities.UI
{
    public class UIBoardItemSelect : GameObject
    {
        private GameObject _buttonSelect1;
        private GameObject _buttonSelect2;
        private GameObject _buttonSelect3;
        private GameObject _buttonConfirm;

        private GameObject _uiText;

        private GameObject _screen;
        private GameObject _background;
        private Color _backgroundColor = (new Vector3(64f, 73f, 115f)).ToColor();

        public override void Start()
        {
            tag = "board item select";

            _background = new GameObject();
            Scene.AddGameObject(_background, Layers.UI);
            _background.AddComponent<UIBackgroundSpriteComponent>().SetSprite(SquareSprite.SquareTexture, _backgroundColor);

            _screen = new GameObject();
            Scene.AddGameObject(_screen, Layers.UI);

            var sprite = _screen.AddComponent<SpriteComponent>();
            sprite.SetAtlas("board item select");
            sprite.Origin = sprite.Sprite.Size.Half();
            sprite.UpdateSprite();

            _screen.Position = Scene.Sizes.ToVector2().Half();

            var initialAnimation = _screen.AddComponent<UIAnimationComponent>();
            initialAnimation.TweenType = Tweening.TweenType.BackEaseOut;
            initialAnimation.MaxScale = 0.0001f;
            initialAnimation.DefaulfScale = 1.0f;
            initialAnimation.AnimationDuration = 0.3f;
            initialAnimation.CalculateOrigin = false;

            _screen.Scale = initialAnimation.MaxScale;
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

            _uiText = new GameObject();
            
            _uiText.Body = new Rectangle(0, 0, 200, 80);
            _uiText.Position = Scene.Sizes.ToVector2().Half() - Vector2.UnitY * (((float)_uiText.Body.Height).Half() + 20f);
            _uiText.Position = new Vector2(_uiText.Position.X - ((float)_uiText.Body.Width).Half(), _uiText.Position.Y);
            _uiText.tag = "text choose your tools";
            Scene.AddGameObject(_uiText, Layers.UI);
            var text = _uiText.AddComponent<UITextComponent>();
            text.SetFont(Content.Load<SpriteFont>("Fonts/FontUIText"));
            text.SetText("Choose your tools");
            text.SetFontSize(0.3f);
            text.SetTextFormt(UITextComponent.TextFormat.CENTER, UITextComponent.TextAlignment.MIDDLE);

            text = _uiText.AddComponent<UITextComponent>();
            text.SetFont(Content.Load<SpriteFont>("Fonts/FontUIText"));
            text.SetText("(0/2)");
            text.SetFontSize(0.2f);
            text.SetTextFormt(UITextComponent.TextFormat.RIGHT, UITextComponent.TextAlignment.TOP);

            var spriteGrid = _screen.AddComponent<UISpriteGridComponent>();
            spriteGrid.SetSize(sprite.Sprite.Size);
            spriteGrid.SetSpacing(new Vector2(5f, _screen.Origin.Y));
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
            _screen.Destroy();
            _background.Destroy();
            _uiText.Destroy();
        }

        public void OnSubmit(ToolsTypes[] chosenTools)
        {
            Destroy();
        }
    }
}
