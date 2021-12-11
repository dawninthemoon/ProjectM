using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class CRTAnimation : VisibleAnimation
    {
        public CRTAnimation(UIRoot root) : base(root)
        {
            float showTime = 0.5f;
            float time = 0.25f;
            Transform transform = root.transform;

            showSequence = DOTween.Sequence()
                            .SetAutoKill(false)
                            .Append(DOTween.To(() => 0f, (value) => root.CanvasGroup.alpha = value, 1f, showTime)
                                .SetEase(Ease.OutBack))
                            .OnComplete(() => {
                                root.CanvasGroup.alpha = 1f;
                                root.SetVisible(true);
                            })
                            .Pause();

            hideSequence = DOTween.Sequence()
                            .SetAutoKill(false)
                            .Append(DOTween.To(() => 1f, (value) => root.CanvasGroup.alpha = value, 0f, time)
                                .SetEase(Ease.OutBack))
                            .OnComplete(() => {
                                root.Close();
                            })
                            .Pause();
        }
    }
}