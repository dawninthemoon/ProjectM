using UnityEngine;
using DG.Tweening;

using Utills;

namespace UI
{
    public class OpenVerticalAnimation : VisibleAnimation
    {
        public OpenVerticalAnimation(UIRoot root) : base(root)
        {
            float time = 0.25f;
            RectTransform rectTransform = (RectTransform)root.transform;

            float height = rectTransform.sizeDelta.y;

            root.CanvasGroup.alpha = 1f;

            showSequence = DOTween.Sequence()
                            .SetAutoKill(false)
                            .Append(DOTween.To(() => 0f, (value) => rectTransform.sizeDelta = rectTransform.sizeDelta.ChangeY(value), height, time)
                                .SetEase(Ease.OutCubic))
                            .OnComplete(() => {
                                root.SetVisible(true);
                            })
                            .Pause();

            hideSequence = DOTween.Sequence()
                            .SetAutoKill(false)
                            .Append(DOTween.To(() => height, (value) => rectTransform.sizeDelta = rectTransform.sizeDelta.ChangeY(value), 0f, time)
                                .SetEase(Ease.OutCubic))
                            .OnComplete(() => {
                                root.Close();
                            })
                            .Pause();
        }
    }
}