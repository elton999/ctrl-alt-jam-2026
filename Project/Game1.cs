using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Entities;
using Project.Entities.Obstacles;
using Project.Entities.UI;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.GameSettings;

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
            _assetManagement = new AssetManagement();
            _gameManagement = new GameManagement(this);
            _gameManagement.Game = this;
            _gameManagement.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetManagement.Set<Player>("Player", Layers.PLAYER);
            _assetManagement.Set<CameraGameObject>("Player", Layers.MIDDLEGROUND);
            _assetManagement.Set<InventoryGameObject>("Player", Layers.MIDDLEGROUND);
            _assetManagement.Set<LevelManagerEntity>("Player", Layers.MIDDLEGROUND);
            _assetManagement.Set<UILevelManagerEntity>("Player", Layers.UI);

            _assetManagement.Set<Tree>("Tree", Layers.MIDDLEGROUND);
            _assetManagement.Set<Stone>("Stone", Layers.MIDDLEGROUND);
            _assetManagement.Set<Barrel>("Barrel", Layers.MIDDLEGROUND);
            _assetManagement.Set<Enemy>("Enemy", Layers.MIDDLEGROUND);

            _gameManagement.SceneManagement.SetScene(0);
            _gameManagement.SceneManagement.MainScene.LevelReady = true;
            _gameManagement.SceneManagement.MainScene.BackgroundColor = new Color(125, 56, 51);

            var inputSettings = GameSettingsProperty.GetProperty<InputGameSettings>(@"Content/" + nameof(InputGameSettings));
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
