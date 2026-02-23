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
            Scene.AddGameObject(_hudBoard, Layers.UI);
            _hudBoard.AddComponent<SpriteComponent>().SetAtlas("hud board");
            _hudBoard.AddComponent<HudLevelAnimation>();

            _countMovement = new GameObject();
            Scene.AddGameObject(_countMovement  , Layers.UI);
            _countMovement.AddComponent<SpriteComponent>().SetAtlas("buoy ui");
            _countMovement.AddComponent<HudLevelAnimation>().SetRenderPosition(HudLevelAnimation.RenderPosition.RIGHT);

            _hudPortrait = new GameObject();
            Scene.AddGameObject(_hudPortrait, Layers.UI);
            _hudPortrait.AddComponent<SpriteComponent>().SetAtlas("portrait hud");
            _hudPortrait.AddComponent<HudLevelAnimation>();
        }
    }
}
