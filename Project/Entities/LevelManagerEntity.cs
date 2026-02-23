using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Entities
{
    public class LevelManagerEntity : GameObject
    {
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

        public override void Start()
        {
            if(Instance == null)
            {
                Instance = this;
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

        public static bool CanRegisterAMove()
        {
            if (Instance == null) return false;

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
