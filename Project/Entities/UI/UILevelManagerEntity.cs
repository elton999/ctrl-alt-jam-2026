using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;

namespace Project.Entities.UI
{
    public class UILevelManagerEntity : GameObject
    {
        private GameObject _hudBoard;

        public override void Start()
        {
            tag = nameof(UILevelManagerEntity);

            _hudBoard = new GameObject();
            Scene.AddGameObject(_hudBoard, Layers.UI);
            _hudBoard.AddComponent<SpriteComponent>().SetAtlas("hud board");
        }
    }
}
