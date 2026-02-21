using System.Collections.Generic;
using UmbrellaToolsKit;

namespace Project.Entities
{
    public class InventoryGameObject : GameObject
    {
        public static InventoryGameObject Instance;

        public Dictionary<ToolsTypes, int> Tools = new Dictionary<ToolsTypes, int>()
        {
            { ToolsTypes.AXE, 100 },
            { ToolsTypes.BOMB, 100 },
            { ToolsTypes.SWORD, 100 },
            { ToolsTypes.BOOT, 0 }
        };

        public override void Start()
        {
            if (Instance == null)
            {
               Instance = this;
            }

            tag = "Inventory";
        }

        public override void OnDestroy()
        {
            Instance = null;
            base.OnDestroy();
        }

        public static bool HasItem(ToolsTypes tool)
        {
            if (Instance == null) return false;
            if (Instance.Tools[tool] > 0) return true;

            return false;
        }

        public static bool UseItem(ToolsTypes tool)
        {
            if (Instance == null) return false;
            if (HasItem(tool))
            {
                Instance.Tools[tool] -= 1;
                return true;
            }

            return false;
        }
    }
}
