using System.Collections;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Components
{
    public class UIButtonToolSelectAnimationComponent : UIAnimationComponent
    {
        [ShowEditor] public float DelayToStartAnimation = 1f;

        public override void Start()
        {
            base.Start();
            TweenType = Tweening.TweenType.BounceEaseOut;
            StartScale = 0.0001f;
            EndScale = 1.0f;
            AnimationDuration = 0.3f;
            CalculateOrigin = false;

            GameObject.Scale = StartScale;

            GameObject.CoroutineManagement.StarCoroutine(OpeningAnimation());
        }

        private IEnumerator OpeningAnimation()
        {
            yield return GameObject.CoroutineManagement.Wait(DelayToStartAnimation);
            StartAnimation();
        }

    }
}
