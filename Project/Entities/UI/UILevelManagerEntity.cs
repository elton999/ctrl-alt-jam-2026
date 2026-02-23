using Microsoft.Xna.Framework.Graphics;
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

        public override void Start()
        {
            tag = nameof(UILevelManagerEntity);

            _hudBoard = new GameObject();
            _hudBoard.tag = "hud board";
            Scene.AddGameObject(_hudBoard, Layers.UI);
            _hudBoard.AddComponent<SpriteComponent>().SetAtlas("hud board");
            _hudBoard.AddComponent<HudLevelAnimation>();

            _countMovement = new GameObject();
            _countMovement.tag = "buoy ui";
            Scene.AddGameObject(_countMovement  , Layers.UI);
            _countMovement.AddComponent<SpriteComponent>().SetAtlas("buoy ui");
            var hudAnimation = _countMovement.AddComponent<HudLevelAnimation>();
            hudAnimation.SetRenderPosition(HudLevelAnimation.RenderPosition.RIGHT);
            hudAnimation.SetAnimationDuration(1.5f);
            _countMovement.AddComponent<UIAnimationByPlayerMovementComponent>();
            var textCountMovement = _countMovement.AddComponent<UITextComponent>();
            textCountMovement.SetFont(Content.Load<SpriteFont>("FontMovementCount"));
            textCountMovement.SetTextFormt(UITextComponent.TextFormat.CENTER, UITextComponent.TextAlignment.MIDDLE);
            _countMovement.AddComponent<ShowMovementsRemainingComponent>();

            _hudPortrait = new GameObject();
            _hudPortrait.tag = "portrait hud";
            Scene.AddGameObject(_hudPortrait, Layers.UI);
            _hudPortrait.AddComponent<SpriteComponent>().SetAtlas("portrait hud");
            _hudPortrait.AddComponent<HudLevelAnimation>().SetAnimationDuration(1.2f);
        }
    }
}
