using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project.Components;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities.UI
{
    public class UILevelManagerEntity : GameObject
    {
        private GameObject _hudBoard;
        private GameObject _countMovement;
        private GameObject _hudPortrait;
        private UIAnimationComponent UIAnimationByPlayerMovement;

        private GameObject _axeCount;
        private GameObject _bombCount;
        private GameObject _bootCount;
        private UISpriteGridComponent _gridItemsComponent;
        private UITextComponent _axeCountText;
        private UITextComponent _bombCountText;
        private UITextComponent _bootCountText;

        public void OnStartLevel()
        {
            _hudBoard = new GameObject();
            _hudBoard.tag = "hud board";
            Scene.AddGameObject(_hudBoard, Layers.UI);
            _hudBoard.AddComponent<SpriteComponent>().SetAtlas("hud board");
            _hudBoard.AddComponent<HudLevelAnimation>().SetRenderPosition(HudLevelAnimation.RenderPosition.LEFT);

            _countMovement = new GameObject();
            _countMovement.tag = "buoy ui";
            Scene.AddGameObject(_countMovement, Layers.UI);
            _countMovement.AddComponent<SpriteComponent>().SetAtlas("buoy ui");

            var hudAnimation = _countMovement.AddComponent<HudLevelAnimation>();
            hudAnimation.SetRenderPosition(HudLevelAnimation.RenderPosition.RIGHT);
            hudAnimation.SetAnimationDuration(1.5f);
            UIAnimationByPlayerMovement = _countMovement.AddComponent<UIAnimationComponent>();

            var textCountMovement = _countMovement.AddComponent<UITextComponent>();
            textCountMovement.SetFont(Content.Load<SpriteFont>("Fonts/FontMovementCount"));
            textCountMovement.SetTextFormt(UITextComponent.TextFormat.CENTER, UITextComponent.TextAlignment.MIDDLE);
            _countMovement.AddComponent<ShowMovementsRemainingComponent>();

            _hudPortrait = new GameObject();
            _hudPortrait.tag = "portrait hud";
            Scene.AddGameObject(_hudPortrait, Layers.UI);
            _hudPortrait.AddComponent<SpriteComponent>().SetAtlas("portrait hud");
            var hudPortraitAnimation = _hudPortrait.AddComponent<HudLevelAnimation>();
            hudPortraitAnimation.SetRenderPosition(HudLevelAnimation.RenderPosition.LEFT);
            hudPortraitAnimation.SetAnimationDuration(1.2f);

            _axeCount = new GameObject();
            _axeCount.tag = "axe count";
            _bombCount = new GameObject();
            _bootCount = new GameObject();

            Scene.AddGameObject(_axeCount, Layers.UI);
            Scene.AddGameObject(_bombCount, Layers.UI);
            Scene.AddGameObject(_bootCount, Layers.UI);

            var axeCountSprite = _axeCount.AddComponent<SpriteComponent>();
            var bombCountSprite = _bombCount.AddComponent<SpriteComponent>();
            var bootCountSprite = _bootCount.AddComponent<SpriteComponent>();

            var font = Content.Load<SpriteFont>("Fonts/FontUIText");
            var countOffset = new Vector2(-2f, 3f);

            _axeCountText = _axeCount.AddComponent<UITextComponent>();
            _axeCountText.SetFont(font);
            _axeCountText.SetFontSize(0.14f);
            _axeCountText.SetTextFormt(UITextComponent.TextFormat.RIGHT, UITextComponent.TextAlignment.TOP);
            _axeCountText.SetText("0");
            _axeCountText.SetOffset(countOffset);

            _bombCountText = _bombCount.AddComponent<UITextComponent>();
            _bombCountText.SetFont(font);
            _bombCountText.SetFontSize(0.16f);
            _bombCountText.SetTextFormt(UITextComponent.TextFormat.RIGHT, UITextComponent.TextAlignment.TOP);
            _bombCountText.SetText("0");
            _bombCountText.SetOffset(countOffset);

            _bootCountText = _bootCount.AddComponent<UITextComponent>();
            _bootCountText.SetFont(font);
            _bootCountText.SetFontSize(0.16f);
            _bootCountText.SetTextFormt(UITextComponent.TextFormat.RIGHT, UITextComponent.TextAlignment.TOP);
            _bootCountText.SetText("0");
            _bootCountText.SetOffset(countOffset);


            axeCountSprite.SetAtlas("axe count" + (InventoryGameObject.HasItem(ToolsTypes.AXE) ? string.Empty : " disable"));
            bombCountSprite.SetAtlas("bomb count" + (InventoryGameObject.HasItem(ToolsTypes.BOMB) ? string.Empty : " disable"));
            bootCountSprite.SetAtlas("boot count" + (InventoryGameObject.HasItem(ToolsTypes.BOOT) ? string.Empty : " disable"));

            var gridItems = new GameObject();
            Scene.AddGameObject(gridItems, Layers.UI);

            var spriteGrid = AddComponent<UISpriteGridComponent>();
            spriteGrid.AddSprite(axeCountSprite);
            spriteGrid.AddSprite(bombCountSprite);
            spriteGrid.AddSprite(bootCountSprite);
            spriteGrid.SetSize(new Vector2(140, 44));
            spriteGrid.GameObject.Position = Scene.Sizes.ToVector2().Half();
            spriteGrid.GameObject.Position -= Vector2.UnitX * (140f / 2f);
            spriteGrid.UpdatePosition();

            var gridAnimationOffset = new Vector2(0, -5);
            var gridItemsAnimation = _axeCount.AddComponent<HudLevelAnimation>();
            gridItemsAnimation.SetAnimationDuration(1.25f);
            gridItemsAnimation.SetOffset(gridAnimationOffset);

            gridItemsAnimation = _bombCount.AddComponent<HudLevelAnimation>();
            gridItemsAnimation.SetAnimationDuration(1.3f);
            gridItemsAnimation.SetOffset(gridAnimationOffset);

            gridItemsAnimation = _bootCount.AddComponent<HudLevelAnimation>();
            gridItemsAnimation.SetAnimationDuration(1.35f);
            gridItemsAnimation.SetOffset(gridAnimationOffset);
            Player.OnPlayerMove += UIAnimationByPlayerMovement.StartAnimation;

            UpdateHudData();
        }

        public override void Start()
        {
            tag = nameof(UILevelManagerEntity);

            ChosenToolsSubmitComponent.OnSubmitChosenTools += OnStartLevelFirstTime;
            Player.OnPlayerMove += OnPlayerMovement;
            Player.OnPlayerMove += UpdateHudData;
        }

        public override void OnDestroy()
        {
            Player.OnPlayerMove -= UIAnimationByPlayerMovement.StartAnimation;
            ChosenToolsSubmitComponent.OnSubmitChosenTools -= OnStartLevelFirstTime;
            Player.OnPlayerMove -= OnPlayerMovement;
            Player.OnPlayerMove -= UpdateHudData;
        }

        private void OnStartLevelFirstTime(ToolsTypes[] tools)
        {
            OnStartLevel();
        }

        private void UpdateHudData()
        {
            _axeCountText.SetText(InventoryGameObject.Instance.Tools[ToolsTypes.AXE].ToString());
            _bombCountText.SetText(InventoryGameObject.Instance.Tools[ToolsTypes.BOMB].ToString());
            _bootCountText.SetText(InventoryGameObject.Instance.Tools[ToolsTypes.BOOT].ToString());
        }

        private void OnPlayerMovement()
        {
            UIUseToolEfx.Instance.PlayMovement(_countMovement.Position);
        }
    }
}
