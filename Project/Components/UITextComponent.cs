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
        private Vector2 _autoOffset;
        [ShowEditor] private Vector2 _offset = Vector2.Zero;
        [ShowEditor] private Color _color = Color.White;
        [ShowEditor] private TextFormat _textFormat;
        [ShowEditor] private TextAlignment _textAligment;
        [ShowEditor] private float _fontSize = 1f;
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
            _autoOffset = Vector2.Zero;

            if (_textFormat is TextFormat.CENTER)
            {
                _autoOffset = new Vector2(GameObject.Body.Width / 2.0f - _textSize.X.Half() * _fontSize, _autoOffset.Y);
            }

            if (_textFormat is TextFormat.RIGHT)
            {
                _autoOffset = new Vector2(GameObject.Body.Width - _textSize.X * _fontSize, _autoOffset.Y);
            }

            if (_textAligment is TextAlignment.MIDDLE)
            {
                _autoOffset = new Vector2(_autoOffset.X, GameObject.Body.Height / 2.0f - _textSize.Y.Half() * _fontSize);
            }

            if (_textAligment is TextAlignment.BOTTOM)
            {
                _autoOffset = new Vector2(_autoOffset.X, GameObject.Body.Height - _textSize.Y * _fontSize);
            }
        }

        public void SetFont(SpriteFont font) => _font = font;

        public void SetFontSize(float fontSize) => _fontSize = fontSize;

        public void SetOffset(Vector2 offset) => _offset = offset;

        public void SetText(string text)
        {
            _text = text;
            if (_font is null) return;
            _textSize = _font.MeasureString(_text);
        }

        public void SetTextFormt(TextFormat textFormat, TextAlignment textAligment)
        {
            _textFormat = textFormat;
            _textAligment = textAligment;
        }

        private void DrawText(SpriteBatch spriteBatch)
        {
            if (_font is null) return;
            if (_text is null) return;

            spriteBatch.DrawString(_font, _text, Vector2.Round(GameObject.Position + _autoOffset + _offset), _color, GameObject.Rotation, Vector2.Zero, _fontSize * GameObject.Scale, SpriteEffects.None, 0f);
        }
    }
}
