using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.Input;

namespace Project.Entities
{
    public class LevelManagerEntity : GameObject
    {
        public enum GameState
        {
            SELECT_TOOLS,
            PLAYING,
            ENDING_LEVEL,
        }

        public static LevelManagerEntity Instance;
        public static float RemainingMovements
        {
            get
            {
                if (Instance is null)
                    return 0;

                return Instance._maxMovements - Instance._currentMovement;
            }
        }
        public static int CurrentLevel
        {
            get
            {
                if (Instance is null)
                    return 0;
                return _currentLevel;
            }
        }

        [ShowEditor] private int _currentMovement = 0;
        private int _maxMovements = 90;
        [ShowEditor] private GameState _currentState = GameState.SELECT_TOOLS;
        [ShowEditor]  private static int _currentLevel = 0;


        public override void Start()
        {
            if(Instance is null)
            {
                Instance = this;
            }

            var levelSettings = GameSettingsProperty.GetProperty<LevelGameSettings>(@"Content/" + nameof(LevelGameSettings));
            if (levelSettings != null)
            {
                _maxMovements = levelSettings.Levels[_currentLevel].MaxMovements;
            }

            tag = "LevelManager";
            Player.OnPlayerMove += RegisterAMove;
        }

        public override void OnDestroy()
        {
            Player.OnPlayerMove -= RegisterAMove;
            Instance = null;
        }

        public override void Update(float deltaTime)
        {
            if (_currentState != GameState.PLAYING) return;
            if (KeyBoardHandler.KeyPressed("reset"))
            {
                Log.Write($"[{nameof(LevelManagerEntity)}] Reset level");
                ResetLevel();
            }
        }

        public static void SetState(GameState state)
        {
            if (Instance is null) return;

            Instance._currentState = state;
        }

        public static bool CanRegisterAMove()
        {
            if (Instance is null) return false;

            if (Instance._currentState != GameState.PLAYING) return false;

            if (Instance._currentMovement < Instance._maxMovements) return true;

            return false;
        }

        public static void RegisterAMove()
        {
            if (Instance is null) return;
            if (!CanRegisterAMove()) return;

            Instance._currentMovement++;
        }

        public static void GoToLevel(int levelIndex)
        {
            if (Instance is null) return;
            _currentLevel = levelIndex;
            Instance.Scene.SceneManagement.SetScene(levelIndex);
        }

        public static void GoToNextLevel()
        {
            if (Instance is null) return;
            _currentLevel++;
            int currentSceneIndex = Instance.Scene.SceneManagement.CurrentScene + 1;
            GoToLevel(currentSceneIndex);
        }

        public static void ResetLevel()
        {
            if (Instance is null) return;
            int currentSceneIndex = Instance.Scene.SceneManagement.CurrentScene;
            GoToLevel(currentSceneIndex);
        }
    }
}
