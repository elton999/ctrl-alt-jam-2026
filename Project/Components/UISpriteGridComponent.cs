using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Components
{
    public class UISpriteGridComponent : Component
    {
        private List<SpriteComponent> _sprites = new List<SpriteComponent>();
        [ShowEditor] private Vector2 _size = new Vector2(100);
        [ShowEditor] private Vector2 _spacing = new Vector2(10);

        public void SetSize(Vector2 newSize)
        {
            _size = newSize;
            UpdatePosition();
        }

        public void SetSpacing(Vector2 newSpacing)
        {
            _spacing = newSpacing;
            UpdatePosition();
        }

        public void AddSprite(SpriteComponent sprite)
        {
            if (_sprites.Contains(sprite)) return;
            _sprites.Add(sprite);
            UpdatePosition();
        }

        public void RemoveSprite(SpriteComponent sprite)
        {
            _sprites.Remove(sprite);
            UpdatePosition();
        }

        [Button]
        public void UpdatePosition()
        {
            if (_sprites.Count == 0) return;

            int weight = 0;
            foreach (var sprite in _sprites)
                weight += (int)sprite.Sprite.Size.X + (int)_spacing.X;

            int offset = (int)((_size.X - weight) / 2.0f);
            int spriteOffset = 0;

            foreach (var sprite in _sprites)
            {
                Vector2 position = GameObject.Position.Truncate() - GameObject.Origin.Truncate();
                position.X += offset + spriteOffset;
                position.Y += _spacing.Y;
                sprite.GameObject.Position = position;

                spriteOffset += (int)sprite.Sprite.Size.X + (int)_spacing.X;
            }
        }
    }
}
