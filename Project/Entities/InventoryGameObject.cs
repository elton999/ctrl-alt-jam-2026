using Project.Components;
using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;

namespace Project.Entities
{
    public class InventoryGameObject : GameObject
    {
        public static InventoryGameObject Instance;

        public Dictionary<ToolsTypes, int> Tools = new Dictionary<ToolsTypes, int>()
        {
            { ToolsTypes.AXE, 0 },
            { ToolsTypes.BOMB, 0 },
            { ToolsTypes.SWORD, int.MaxValue },
            { ToolsTypes.BOOT, 0 }
        };

        public override void Start()
        {
            if (Instance == null)
            {
               Instance = this;
            }

            tag = nameof(InventoryGameObject);

            ChosenToolsSubmitComponent.OnSubmitChosenTools += OnSubmit;
        }

        public override void OnDestroy()
        {
            ChosenToolsSubmitComponent.OnSubmitChosenTools -= OnSubmit;
            Instance = null;
            base.OnDestroy();
        }

        public void OnSubmit(ToolsTypes[] chosenTools)
        {
            foreach (var tool in chosenTools)
            {
                UseItem(tool);
            }
        }

        public void ResetInventory()
        {
            var levelSettings = GameSettingsProperty.GetProperty<LevelGameSettings>(@"Content/" + nameof(LevelGameSettings));
            var toolsLimitations = levelSettings.Levels[0].ToolsLimitations;

            Tools[ToolsTypes.AXE] = toolsLimitations.Find(x => x.Tool == ToolsTypes.AXE).Max;
            Tools[ToolsTypes.BOMB] = toolsLimitations.Find(x => x.Tool == ToolsTypes.BOMB).Max;
            Tools[ToolsTypes.SWORD] = toolsLimitations.Find(x => x.Tool == ToolsTypes.SWORD).Max;
            Tools[ToolsTypes.BOOT] = toolsLimitations.Find(x => x.Tool == ToolsTypes.BOOT).Max;
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
