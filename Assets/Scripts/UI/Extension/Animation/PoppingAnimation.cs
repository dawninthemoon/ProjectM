using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class PoppingAnimation : VisibleAnimation
    {
        public PoppingAnimation(UIRoot root) : base(root)
        {
            float time = 0.25f;
            Transform transform = root.transform;

            showSequence = DOTween.Sequence()
                            .SetAutoKill(false)
                            .Append(DOTween.To(() => 0.25f, (value) => transform.localScale = Vector3.one * value, 1f, time)
                                .SetEase(Ease.OutBack))
                            .Join(DOTween.To(() => 0f, (value) => root.CanvasGroup.alpha = value, 1f, time)
                                .SetEase(Ease.OutBack))
                            .OnComplete(() => {
                                root.SetVisible(true);
                            })
                            .Pause();

            hideSequence = DOTween.Sequence()
                            .SetAutoKill(false)
                            .Append(DOTween.To(() => 1f, (value) => transform.localScale = Vector3.one * value, 0.25f, time)
                                .SetEase(Ease.OutBack))
                            .Join(DOTween.To(() => 1f, (value) => root.CanvasGroup.alpha = value, 0f, time)
                                .SetEase(Ease.OutBack))
                            .OnComplete(() => {
                                root.Close();
                            })
                            .Pause();
        }
    }
}