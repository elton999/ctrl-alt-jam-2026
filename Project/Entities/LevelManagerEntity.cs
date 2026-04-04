using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;

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
                if (Instance == null)
                    return 0;

                return Instance._maxMovements - Instance._currentMovement;
            }
        }

        [ShowEditor] private int _currentMovement = 0;
        private int _maxMovements = 90;
        [ShowEditor] private GameState _currentState = GameState.SELECT_TOOLS;

        public override void Start()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            var levelSettings = GameSettingsProperty.GetProperty<LevelGameSettings>(@"Content/" + nameof(LevelGameSettings));
            if (levelSettings != null)
            {
                _maxMovements = levelSettings.Levels[0].MaxMovements;
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

        }

        public static void SetState(GameState state)
        {
            if (Instance == null) return;

            Instance._currentState = state;
        }

        public static bool CanRegisterAMove()
        {
            if (Instance == null) return false;

            if (Instance._currentState != GameState.PLAYING) return false;

            if (Instance._currentMovement < Instance._maxMovements) return true;

            return false;
        }

        public static void RegisterAMove()
        {
            if (Instance == null) return;
            if (!CanRegisterAMove()) return;

            Instance._currentMovement++;
        }
    }
}
