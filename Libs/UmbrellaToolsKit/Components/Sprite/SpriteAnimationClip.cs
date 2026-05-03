using System.Collections.Generic;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.GameSettings;

namespace UmbrellaToolsKit.Components.Sprite
{
    public class SpriteAnimationClip
    {
        public List<SpriteFrame> Frames = new();
        public string ClipName;
        public AnimationDirection AnimationDirection = AnimationDirection.FORWARD;

        public SpriteAnimationClip(Sprite[] sprites, float durationByFrame, string name = "default")
        {
            foreach (var sprite in sprites)
            {
                Frames.Add(new SpriteFrame()
                {
                    Duration = durationByFrame,
                    Sprite = sprite,
                });

                ClipName = name;
            }
        }

        public SpriteAnimationClip(string spriteName, int frameNumber, float duration, string name = "default")
        {
            ClipName = name;
            for (int frameCount = 1; frameCount <= frameNumber; frameCount++)
            {
                var atlas = GameSettingsProperty.GetProperty<AtlasGameSettings>(@"Content/AtlasGameSettings");
                if (atlas.TryGetSpriteByName($"{spriteName} {frameCount}", out var sprite))
                {
                    var spriteData = new Sprite(sprite.Name, sprite.Path, sprite.GetRectangle());
                    var frame = new SpriteFrame() { Sprite = spriteData, Duration = duration };

                    Frames.Add(frame);
                }
            }
        }
    }
}
