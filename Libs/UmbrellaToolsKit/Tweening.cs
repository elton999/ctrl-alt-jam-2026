using Microsoft.Xna.Framework;
using System;

namespace UmbrellaToolsKit
{
    public class Tweening
    {
        public static float Lerp(float min, float max, float value) => min + (max - min) * value;

        public static Vector2 Lerp(Vector2 min, Vector2 max, float value) => min + (max - min) * value;

        public static float LinearTween(float b, float c, float time, float duration) => c * time / duration + b;

        public static float EaseInQuad(float b, float c, float time, float duration) => c * (time /= duration) * time + b;

        public static float EaseOutQuad(float b, float c, float time, float duration) => -c * (time /= duration) * (time - 2) + b;

        public static float ElasticEaseOut(float b, float c, float time, float duration)
        {
            if ((time /= duration) == 1.0f)
                return b + c;
            float num1 = duration * 0.3f;
            float num2 = num1 / 4.0f;
            return c * (float)Math.Pow(2.0, -10.0 * (double)time) * (float)Math.Sin((double)((time * duration - num2) * (2.0f * (float)Math.PI) / num1)) + c + b;
        }

        public static float ElasticEaseIn(float b, float c, float time, float duration)
        {
            if ((time /= duration) == 1.0f)
                return b + c;
            float num1 = duration * 0.3f;
            float num2 = num1 / 4.0f;
            return -(c * (float)Math.Pow(2.0, 10.0 * --time) * (float)Math.Sin((time * duration - num2) * (2.0 * Math.PI) / num1)) + b;
        }

        public static float ElasticEaseInOut(float b, float c, float time, float duration)
        {
            if ((time /= duration / 2.0f) == 2.0f)
                return b + c;
            float num1 = duration * (9.0f / 20.0f);
            float num2 = num1 / 4.0f;
            return time < 1.0f ? -0.5f * (c * (float)Math.Pow(2.0, 10.0 * --time) * (float)Math.Sin((time * duration - num2) * (2.0 * Math.PI) / num1)) + b : c * (float)Math.Pow(2.0, -10.0 * --time) * (float)(float)Math.Sin((time * duration - num2) * (2.0f * (float)Math.PI) / num1) * 0.5f + c + b;
        }

        public static float ElasticEaseOutIn(float b, float c, float time, float duration)
        {
            return time < duration / 2.0 ? Tweening.ElasticEaseOut(time * 2.0f, b, c / 2.0f, duration) : Tweening.ElasticEaseIn(time * 2.0f - duration, b + c / 2.0f, c / 2.0f, duration);
        }

        public static float BounceEaseOut(float b, float c, float time, float duration)
        {
            if ((time /= duration) < (1 / 2.75f)) return c * (7.5625f * time * time) + b;
            else if (time < (2 / 2.75f)) return c * (7.5625f * (time -= (1.5f / 2.75f)) * time + .75f) + b;
            else if (time < (2.5 / 2.75f)) return c * (7.5625f * (time -= (2.25f / 2.75f)) * time + .9375f) + b;
            else return c * (7.5625f * (time -= (2.625f / 2.75f)) * time + .984375f) + b;
        }

        public static float BounceEaseIn(float b, float c, float time, float duration)
        {
            return c - Tweening.BounceEaseOut(duration - time, 0.0f, c, duration) + b;
        }

        public static float BounceEaseInOut(float b, float c, float time, float duration)
        {
            return time < duration / 2.0 ? Tweening.BounceEaseIn(time * 2.0f, 0.0f, c, duration) * 0.5f + b : Tweening.BounceEaseOut(time * 2.0f - duration, 0.0f, c, duration) * 0.5f + c * 0.5f + b;
        }

        public static float BounceEaseOutIn(float b, float c, float time, float duration)
        {
            return time < duration / 2.0 ? Tweening.BounceEaseOut(time * 2.0f, b, c / 2.0f, duration) : Tweening.BounceEaseIn(time * 2.0f - duration, b + c / 2.0f, c / 2.0f, duration);
        }

        public static float BackEaseOut(float b, float c, float time, float duration)
        {
            return c * ((time = time / duration - 1.0f) * time * (2.70158f * time + 1.70158f) + 1.0f) + b;
        }

        public static float BackEaseIn(float b, float c, float time, float duration)
        {
            return c * (time /= duration) * time * (2.70158f * time - 1.70158f) + b;
        }

        public static float BackEaseInOut(float b, float c, float time, float duration)
        {
            float num1 = 1.70158f;
            float num2;
            float num3;
            return (time /= duration / 2.0f) < 1.0f ? c / 2.0f * (time * time * (((num2 = num1 * 1.525f) + 1.0f) * time - num2)) + b : c / 2.0f * ((time -= 2.0f) * time * (((num3 = num1 * 1.525f) + 1.0f) * time + num3) + 2.0f) + b;
        }

        public static float BackEaseOutIn(float b, float c, float time, float duration)
        {
            return time < duration / 2.0 ? Tweening.BackEaseOut(time * 2.0f, b, c / 2.0f, duration) : Tweening.BackEaseIn(time * 2.0f - duration, b + c / 2.0f, c / 2.0f, duration);
        }
    }
}