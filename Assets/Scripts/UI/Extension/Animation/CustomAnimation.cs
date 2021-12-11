using UnityEngine;

namespace UI
{
    public class CustomAnimation : VisibleAnimation
    {
        private Animator animator;
        private AnimationEvent animationEvent;

        public CustomAnimation(UIRoot root) : base(root)
        {
            animator = root.GetComponent<Animator>();
            animationEvent = animator.GetComponent<AnimationEvent>();

            animationEvent.ModeCallback += ChangeMode;
        }

        ~CustomAnimation()
        {
            animationEvent.ModeCallback -= ChangeMode;
        }

        public override void Show()
        {
            root.SetVisible(true);
            animator.Rebind();
            animator.SetTrigger("show");
        }

        public override void Hide()
        {
            // animator.Rebind();
            // animator.SetTrigger("hide");
            root.Close();
        }

        private void ChangeMode(AnimationMode mode)
        {
            if (mode != AnimationMode.End)
                return;
        }
    }
}
