using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Components
{
    public class UITextComponent : Component
    {
        public enum TextFormat { LEFT, CENTER, RIGHT };
        public enum TextAlignment { TOP, MIDDLE, BOTTOM };

        [ShowEditor] private string _text;
        [ShowEditor] private Vector2 _offset;
        [ShowEditor] private Color _color = Color.White;
        [ShowEditor] private TextFormat _textFormat;
        [ShowEditor] private TextAlignment _textAligment;
        private Vector2 _textSize;
        private SpriteFont _font;

        public override void Start()
        {
            GameObject.ExtraDraw += DrawText;
        }

        public override void OnDestroy()
        {
            GameObject.ExtraDraw -= DrawText;
        }

        public override void Update(float deltaTime)
        {
            _offset = Vector2.Zero;

            if (_textFormat is TextFormat.CENTER)
            {
                _offset = new Vector2(GameObject.Body.Width / 2.0f - _textSize.X / 2.0f, _offset.Y);
            }

            if (_textFormat is TextFormat.RIGHT)
            {
                _offset = new Vector2(GameObject.Body.Width - _textSize.X, _offset.Y);
            }

            if (_textAligment is TextAlignment.MIDDLE)
            {
                _offset = new Vector2(_offset.X, GameObject.Body.Height / 2.0f - _textSize.Y / 2.0f);
            }

            if (_textAligment is TextAlignment.BOTTOM)
            {
                _offset = new Vector2(_offset.X, GameObject.Body.Height - _textSize.Y);
            }
        }

        public void SetFont(SpriteFont font) => _font = font;

        public void SetText(string text)
        {
            _text = text;
            if (_font == null) return;
            _textSize = _font.MeasureString(_text);
        }

        public void SetTextFormt(TextFormat textFormat, TextAlignment textAligment)
        {
            _textFormat = textFormat;
            _textAligment = textAligment;
        }

        private void DrawText(SpriteBatch spriteBatch)
        {
            if (_font == null) return;
            spriteBatch.DrawString(_font, _text, GameObject.Position + _offset, _color);
        }
    }
}
