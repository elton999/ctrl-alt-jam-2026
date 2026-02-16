using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine.Windows.GameSettings;

namespace UmbrellaToolsKit.Components.Sprite
{
    public class SpriteComponent : Component
    {
        private string _tempSprite = string.Empty;
        [ShowEditor] private Sprite _sprite;
        [ShowEditor] private float _transparent = 1.0f;
        [ShowEditor] private Vector2 _origin;
        [ShowEditor] private SpriteEffects _spriteEffect = SpriteEffects.None;

        public float Transparent { get => _transparent; set => _transparent = value; }
        public Vector2 Origin { get => _origin; set => _origin = value; }
        public SpriteEffects SpriteEffect { get => _spriteEffect; set => _spriteEffect = value; }

        public override void AfterUpdate(float deltaTime)
        {
            if (_sprite != null && _tempSprite != _sprite.Name)
            {
                SetSprite(_sprite);
            }

            UpdateSprite();
        }

        public void SetSprite(string path) => _sprite = new Sprite(GameObject.Content, path);

        public void SetAtlas(string spriteName)
        {
            var atlas = GameSettingsProperty.GetProperty<AtlasGameSettings>(@"Content/AtlasGameSettings");
            if (atlas.TryGetSpriteByName(spriteName, out var sprite))
            {
                Log.Write($"[{nameof(SpriteComponent)}] creating sprite: path : {sprite.Path} rectangle: {sprite.GetRectangle()}" + spriteName);
                var spriteData = new Sprite(sprite.Name, sprite.Path, sprite.GetRectangle());
                SetSprite(spriteData);
                return;
            }

            Log.Write($"[{nameof(SpriteComponent)}] Sprite not found in atlas: " + spriteName);
        }

        public void SetSprite(Sprite sprite)
        {
            _sprite = sprite;
            _tempSprite = sprite.Name;
            _sprite.SetContentManager(GameObject.Content);
        }

        private void UpdateSprite()
        {
            if (_sprite == null) return;

            GameObject.Sprite = _sprite.Texture;
            GameObject.Transparent = Transparent;
            GameObject.SpriteEffect = SpriteEffect;
            GameObject.Origin = Origin;
            GameObject.Body = _sprite.Body;
        }
    }
}
