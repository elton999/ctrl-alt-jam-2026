using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Components
{
    public class UITextComponent : Component
    {
        [ShowEditor] private string _text;
        [ShowEditor] private Vector2 _offset;
        [ShowEditor] private Color _color = Color.White;
        private SpriteFont _font;

        public override void Start()
        {
            GameObject.ExtraDraw += DrawText;
        }

        public override void OnDestroy()
        {
            GameObject.ExtraDraw -= DrawText;
        }

        public void SetFont(SpriteFont font) => _font = font;

        public void SetText(string text) => _text = text;

        private void DrawText(SpriteBatch spriteBatch)
        {
            if (_font == null) return;
            spriteBatch.DrawString(_font, _text, GameObject.Position - _offset, _color);
        }
    }
}
