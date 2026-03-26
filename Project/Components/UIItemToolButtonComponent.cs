using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine.GameSettings;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class UIItemToolButtonComponent : Component
    {
        [ShowEditor] private ToolsTypes _tool;
        [ShowEditor] private bool _isSelected;

        [ShowEditor] private Sprite _availableSprite;
        [ShowEditor] private Sprite _selectedSprite;

        private ChosenToolsSubmitComponent _chosenToolsSubmitComponent;

        public bool IsSelected => _isSelected;
        public ToolsTypes Tool => _tool;

        public override void Start()
        {
            _availableSprite = GameObject.GetComponent<SpriteComponent>().Sprite;

            var atlas = GameSettingsProperty.GetProperty<AtlasGameSettings>(@"Content/AtlasGameSettings");
            if (atlas.TryGetSpriteByName(_availableSprite.Name + " selected", out var sprite))
            {
                _selectedSprite = new Sprite(sprite);
            }
        }

        public override void Update(float deltaTime)
        {
            if (_tool is ToolsTypes.AXE && KeyBoardHandler.KeyPressed("select 1"))
            {
                ToggleSelected();
                Log.Write($"[{nameof(UIItemToolButtonComponent)}] selected AXE");
                return;
            }
            if (_tool is ToolsTypes.BOMB && KeyBoardHandler.KeyPressed("select 2"))
            {
                ToggleSelected();
                Log.Write($"[{nameof(UIItemToolButtonComponent)}] selected BOMB");
                return;
            }
            if (_tool is ToolsTypes.BOOT && KeyBoardHandler.KeyPressed("select 3"))
            {
                ToggleSelected();
                Log.Write($"[{nameof(UIItemToolButtonComponent)}] selected BOOT");
                return;
            }
        }

        public void SetTool(ToolsTypes tool) => _tool = tool;

        public void SetChosenToolsSubmitComponent(ChosenToolsSubmitComponent chosenToolsSubmitComponent) => _chosenToolsSubmitComponent = chosenToolsSubmitComponent;

        public ToolsTypes GetTool() => _tool;

        public void ToggleSelected()
        {
            if(!IsSelected && _chosenToolsSubmitComponent.HasChosenTools())
            {
                return;
            }
            _isSelected = !_isSelected;
            GameObject.GetComponent<SpriteComponent>().SetSprite(IsSelected ? _selectedSprite : _availableSprite);
        }
    }
}
