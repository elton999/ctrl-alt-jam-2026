using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace UmbrellaToolsKit.GameSettings
{
    [Serializable]
    public struct ScreenSize
    {
        public ScreenSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        [ShowEditor] public int Width;
        [ShowEditor] public int Height;
    }

    [Serializable]
    public struct SceneList
    {
        [ShowEditor] public bool UseTileMapSystem;
        [ShowEditor] public int LevelNumber;
        [ShowEditor] public Color BackgroundColor;
    }

    public enum TileMapIntegration
    {
        NONE, OGMO, LDTK
    }

    [GameSettingsProperty(nameof(BuildGameSettings), "/Content/")]
    public class BuildGameSettings : GameSettingsProperty
    {
        [ShowEditor] public List<SceneList> SceneList = new List<SceneList>() 
        { 
            new SceneList()
            {
                UseTileMapSystem = false,
                LevelNumber = 0,
                BackgroundColor = Color.CornflowerBlue
            }
        };
        [ShowEditor] public List<ScreenSize> Resolutions = new List<ScreenSize>() 
        { 
            new ScreenSize(640, 480),
            new ScreenSize(720, 576),
            new ScreenSize(1920, 1080)
        };
        [ShowEditor] public ScreenSize CanvasScreen = new ScreenSize(640, 480);
        [ShowEditor] public bool FullScreen;
        [ShowEditor] public TileMapIntegration TileMapIntegration;
        [ShowEditor] public int CellSize = 8;
    }
}
