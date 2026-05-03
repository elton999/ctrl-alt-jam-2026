using System.Collections.Generic;

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
    }
}
