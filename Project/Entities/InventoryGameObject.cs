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

        public LevelGameSettings LevelSettings => GameSettingsProperty.GetProperty<LevelGameSettings>(@"Content/" + nameof(LevelGameSettings));
        public List<LevelGameSettings.ToolsLimitations> ToolsLimitations => LevelSettings.Levels[LevelManagerEntity.CurrentLevel].ToolsLimitations;

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
            ResetInventory();
            foreach (var tool in chosenTools)
            {
                Tools[tool] = ToolsLimitations.Find(x => x.Tool == tool).Max;
            }
            LevelManagerEntity.SetState(LevelManagerEntity.GameState.PLAYING);
        }

        public void ResetInventory()
        {
            Tools[ToolsTypes.AXE] = 0;
            Tools[ToolsTypes.BOMB] = 0;
            Tools[ToolsTypes.BOOT] = 0;
            Tools[ToolsTypes.SWORD] = ToolsLimitations.Find(x => x.Tool == ToolsTypes.SWORD).Max;
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
