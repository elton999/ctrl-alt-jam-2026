using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UmbrellaToolsKit.Components.Sprite
{
    public class SquareSprite
    {
        private static Sprite _sprite;

        public static Texture2D SquareTexture;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            CreateSquareTexture(graphicsDevice, 1, Color.White);
        }

        public static void CreateSquareTexture(GraphicsDevice graphicsDevice, int size, Color color)
        {
            SquareTexture = new Texture2D(graphicsDevice, size, size);
            Color[] data = new Color[size * size];
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;
            SquareTexture.SetData(data);
        }

        public static Sprite GetSprite()
        {
            if (_sprite == null)
            {
                _sprite = new Sprite(SquareTexture);
            }
            return _sprite;
        }
    }
}
