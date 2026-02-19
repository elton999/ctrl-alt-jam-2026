using System;
using System.Collections.Generic;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project
{
    [GameSettingsProperty(nameof(LevelGameSettings), "/Content/")]
    public class LevelGameSettings : GameSettingsProperty
    {
        [Serializable]
        public class ToolsLimitations
        {
            [ShowEditor] public ToolsTypes Tool;
            [ShowEditor] public int Max;
        }

        [Serializable]
        public class Level
        {
            [ShowEditor] public List<ToolsLimitations> ToolsLimitations = new List<ToolsLimitations>();
            [ShowEditor] public int MaxMovements;
            [ShowEditor] public int LevelNumberIntegration;
            [ShowEditor] public int Index;
        }

        [ShowEditor] public List<Level> Levels = new List<Level>();

        public Level DefaulfLevel
        {
            get
            {
                return new Level()
                {
                    ToolsLimitations = new List<ToolsLimitations>()
                    {
                        new ToolsLimitations() { Tool = ToolsTypes.AXE, 
                        Max = 9},
                        new ToolsLimitations() { Tool = ToolsTypes.BOMB,
                        Max = 9},
                        new ToolsLimitations() { Tool = ToolsTypes.SWORD,
                        Max = 9},
                        new ToolsLimitations() { Tool = ToolsTypes.BOOT,
                        Max = 9},
                    }
                };
            }
        }

        public Level GetLevelByIndex(int index)
        {
            foreach (var level in Levels)
            {
                if (level.Index == index)
                {
                    return level;
                }
            }

            return DefaulfLevel;
        }
    }
}
