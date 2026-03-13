using System;
using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;

namespace Project.Components
{
    public class ChosenToolsSubmitComponent : Component
    {
        public static event Action<ToolsTypes[]> OnSubmitChosenTools;

        private UIItemToolButtonComponent[] _toolButtons;
        private const int MIN_TOOLS_TO_CHOOSE = 2;

        public override void Start()
        {
            _toolButtons = GameObject.Scene.UI
                .FindAll(go => go.GetComponent<UIItemToolButtonComponent>() != null)
                .ConvertAll(go => go.GetComponent<UIItemToolButtonComponent>()).ToArray();
        }

        public void SubmitChosenTools()
        {
            if (!HasChosenTools()) return;

            foreach (var toolButton in _toolButtons)
            {
                if (toolButton.IsSelected)
                {
                    Log.Write($"Chosen tool: {toolButton.Tool}");
                    OnSubmitChosenTools?.Invoke(GetChosenTools());
                }
            }
        }

        public ToolsTypes[] GetChosenTools()
        {
            var chosenTools = new List<ToolsTypes>();
            foreach (var toolButton in _toolButtons)
            {
                if (toolButton.IsSelected)
                {
                    chosenTools.Add(toolButton.Tool);
                }
            }
            return chosenTools.ToArray();
        }

        public bool HasChosenTools()
        {
            return MIN_TOOLS_TO_CHOOSE <= GetChosenTools().Length;
        }
    }
}
