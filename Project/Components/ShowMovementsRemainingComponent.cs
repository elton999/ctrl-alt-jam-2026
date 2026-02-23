using Project.Entities;
using UmbrellaToolsKit;

namespace Project.Components
{
    public class ShowMovementsRemainingComponent : Component
    {
        private UITextComponent _textComponent;

        public override void Start()
        {
            _textComponent = GameObject.GetComponent<UITextComponent>();
        }

        public override void Update(float deltaTime)
        {
            _textComponent.SetText(LevelManagerEntity.RemainingMovements.ToString());
        }
    }
}
