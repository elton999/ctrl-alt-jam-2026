using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class UIItemToolButtonComponent : Component
    {
        [ShowEditor] private ToolsTypes _tool;
        [ShowEditor] private bool _isSelected;

        public bool IsSelected => _isSelected;
        public ToolsTypes Tool => _tool;

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
