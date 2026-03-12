using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class UIItemToolButtonComponent : Component
    {
        [ShowEditor] private ToolsTypes _tool;
        [ShowEditor] private bool _isSelected;
        private SpriteComponent _spriteComponent;

        public override void Start()
        {
            SpriteComponent spriteComponent = GameObject.GetComponent<SpriteComponent>();
            if (spriteComponent != null)
            {
                _spriteComponent = spriteComponent;
                _spriteComponent.SetAtlas("debug sprite");
                return;
            }
            Log.Write($"[{nameof(UIItemToolButtonComponent)}] requires a SpriteComponent.");
        }

        public override void Update(float deltaTime)
        {
            if (_tool is ToolsTypes.AXE && KeyBoardHandler.KeyPressed("select 1"))
            {
                Log.Write($"[{nameof(UIItemToolButtonComponent)}] selected AXE");
                _isSelected = !_isSelected;
                return;
            }
            if (_tool is ToolsTypes.BOMB && KeyBoardHandler.KeyPressed("select 2"))
            {
                _isSelected = !_isSelected;
                Log.Write($"[{nameof(UIItemToolButtonComponent)}] selected BOMB");
                return;
            }
            if (_tool is ToolsTypes.BOOT && KeyBoardHandler.KeyPressed("select 3"))
            {
                _isSelected = !_isSelected;
                Log.Write($"[{nameof(UIItemToolButtonComponent)}] selected BOOT");
                return;
            }
        }

        public void SetTool(ToolsTypes tool) => _tool = tool;

        public ToolsTypes GetTool() => _tool;
    }
}
