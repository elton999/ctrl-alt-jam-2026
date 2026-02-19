using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Entities
{
    public class LevelManagerEntity : GameObject
    {
        public static LevelManagerEntity Instance;
        [ShowEditor] private int _currentMovement = 0;
        private int _maxMovements = 0;

        public override void Start()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            tag = "LevelManager";
        }

        public override void Update(float deltaTime)
        {
            
        }

        public override void OnDestroy()
        {
            Instance = null;
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
