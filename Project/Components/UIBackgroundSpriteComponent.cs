using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class UIBackgroundSpriteComponent : Component
    {
        public void SetSprite(Texture2D sprite, Color color)
        {
            GameObject.Sprite = sprite;
            GameObject.SpriteColor = color;

            GameObject.Scale = GameObject.Scene.Sizes.X; 
        }
    }
}
