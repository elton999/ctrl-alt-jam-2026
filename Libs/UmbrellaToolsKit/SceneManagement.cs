using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.GameSettings;

namespace UmbrellaToolsKit
{
    public class SceneManagement : Interfaces.IDrawable
    {
        public GameManagement GameManagement;
        public Scene MainScene;
        public int CurrentScene = 0;
        private BuildGameSettings _buildGameSettings;

        public Action<SpriteBatch> ExtraDraw { get; set; }

        public virtual void Start()
        {
            if (_buildGameSettings is null)
            {
                _buildGameSettings = GameSettingsProperty.GetProperty<BuildGameSettings>(@"Content/" + nameof(BuildGameSettings));
            }

            SetScene(CurrentScene);
        }

        public virtual void SetScene(int sceneIndex)
        {
            if (MainScene is not null)
            {
                MainScene.Dispose();
            }

            MainScene = new Scene(
                GameManagement.Game.GraphicsDevice,
                GameManagement.Game.Content
            );

            if (_buildGameSettings is not null)
            {
                MainScene.SetSizes(_buildGameSettings.CanvasScreen.Width, _buildGameSettings.CanvasScreen.Height);
                MainScene.CellSize = _buildGameSettings.CellSize;

                if (_buildGameSettings.SceneList.Count > 0)
                {
                    var scene = _buildGameSettings.SceneList[sceneIndex];
                    if (scene.UseTileMapSystem)
                    {
                        int tileMapIndex = scene.LevelNumber;
                        if (_buildGameSettings.TileMapIntegration is TileMapIntegration.LDTK)
                            MainScene.SetLevelLdtk(tileMapIndex);
                    }
                }
            }
            
            MainScene.GameManagement = GameManagement;
        }

        public void Update(GameTime gameTime)
        {
            if (MainScene != null && MainScene.LevelReady)
                MainScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (MainScene != null && MainScene.LevelReady)
            {
                MainScene.Draw(spriteBatch,
                GameManagement.Game.GraphicsDevice,
                new Vector2(
                    GameManagement.Game.GraphicsDevice.Viewport.Width,
                    GameManagement.Game.GraphicsDevice.Viewport.Height
                ));

            }
        }
    }
}