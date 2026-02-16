using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Components;
using Project.Entities;

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

            _gameManagement.SceneManagement.MainScene.AddGameObject(new GameObject(), Layers.FOREGROUND);
            _gameManagement.SceneManagement.MainScene.LevelReady = true;
            _gameManagement.SceneManagement.MainScene.CellSize = 22;
            _gameManagement.SceneManagement.MainScene.SetSizes(512, 288);
            _gameManagement.SceneManagement.MainScene.BackgroundColor = new Color(125, 56, 51);
            _gameManagement.SceneManagement.MainScene.SetLevelLdtk(0);
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
