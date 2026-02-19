using UmbrellaToolsKit;

namespace Project.Entities
{
    public class LevelManagerEntity : GameObject
    {
        public static LevelManagerEntity Instance;

        public override void Start()
        {
            if(Instance == null)
            {
                Instance = new LevelManagerEntity();
            }
        }

        public override void OnDestroy()
        {
            Instance = null;
        }
    }
}
