using UnityEngine;
using DG.Tweening;

namespace UI
{
    public abstract class VisibleAnimation
    {
        public enum AnimationType
        {
            None,
            Popping,
            Custom,
            CRT,
            OpenVertical,
        }

        protected Sequence showSequence;
        protected Sequence hideSequence;

        protected UIRoot root;

        public VisibleAnimation(UIRoot root)
        {
            this.root = root;
        }

        public virtual void Show()
        {
            if (showSequence == null ||
                hideSequence == null)
                return;
                
            root.CanvasGroup.blocksRaycasts = true;

            hideSequence.Pause();
            showSequence.Restart();
        }

        public virtual void Hide()
        {
            if (showSequence == null ||
                hideSequence == null)
                return;
  
            showSequence.Pause();
            hideSequence.Restart();
        }
    }
}
