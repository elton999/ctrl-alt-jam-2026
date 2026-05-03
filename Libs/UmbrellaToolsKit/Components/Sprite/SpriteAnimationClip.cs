using System.Collections.Generic;

namespace UmbrellaToolsKit.Components.Sprite
{
    public class SpriteAnimationClip
    {
        public List<SpriteFrame> Frames = new();
        public string ClipName;
        public AnimationDirection AnimationDirection = AnimationDirection.FORWARD;
    }
}
