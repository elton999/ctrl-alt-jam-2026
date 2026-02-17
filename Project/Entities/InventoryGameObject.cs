using UmbrellaToolsKit;

namespace Project.Entities
{
    public class InventoryGameObject : GameObject
    {
        public static InventoryGameObject Instance;

        public override void Start()
        {
            Instance = this;
        }

        public override void OnDestroy()
        {
            Instance = null;
            base.OnDestroy();
        }
    }
}
