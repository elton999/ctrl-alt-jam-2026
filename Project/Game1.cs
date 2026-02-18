using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Components;
using Project.Entities;
using Project.Entities.Obstacles;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Windows.GameSettings;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetManagement _assetManagement;
        private GameManagement _gameManagement;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameManagement = new GameManagement(this);
            _gameManagement.Game = this;
            _gameManagement.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetManagement = new AssetManagement();

            _assetManagement.Set<Player>("Player", Layers.PLAYER);
            _assetManagement.Set<CameraGameObject>("Player", Layers.MIDDLEGROUND);
            _assetManagement.Set<InventoryGameObject>("Player", Layers.MIDDLEGROUND);

            _assetManagement.Set<Tree>("Tree", Layers.MIDDLEGROUND);
            _assetManagement.Set<Stone>("Stone", Layers.MIDDLEGROUND);
            _assetManagement.Set<Barrel>("Barrel", Layers.MIDDLEGROUND);
            _assetManagement.Set<Enemy>("Enemy", Layers.MIDDLEGROUND);

            _gameManagement.SceneManagement.MainScene.AddGameObject(new GameObject(), Layers.FOREGROUND);
            _gameManagement.SceneManagement.MainScene.LevelReady = true;
            _gameManagement.SceneManagement.MainScene.CellSize = 22;
            _gameManagement.SceneManagement.MainScene.SetSizes(512, 288);
            _gameManagement.SceneManagement.MainScene.BackgroundColor = new Color(125, 56, 51);
            _gameManagement.SceneManagement.MainScene.SetLevelLdtk(0);

            var inputSettings = GameSettingsProperty.GetProperty<InputGameSettings>(@"Content/InputGameSettings");
            inputSettings.BindAllInputs();
        }

        protected override void Update(GameTime gameTime)
        {
            _gameManagement.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameManagement.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
