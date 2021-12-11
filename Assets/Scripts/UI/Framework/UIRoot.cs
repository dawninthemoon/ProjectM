using System;
using UnityEngine;
using UnityEngine.UI;
using Utills;
using static UI.VisibleAnimation;

namespace UI
{
    public abstract class UIRoot : UIBehaviour
    {
        [SerializeField] private AnimationType visibleType = AnimationType.None;

        public readonly UIElem<Button> HideBtn = new UIElem<Button>(true);
        public readonly UIElem<Button> BtnHide = new UIElem<Button>(true);
        public readonly UIElem<Button> BtnBlocker = new UIElem<Button>(true);

        public Action OnClosed { get; }

        private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup => _canvasGroup;

        private VisibleAnimation animation;
        private bool isHiding = false;

        protected override void Awake()
        {
            base.Awake();

            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            _canvasGroup.SetVisible(false);

            BindHideBtn();
            SetVisibleAnimation();
        }

        private void BindHideBtn()
        {
            if (HideBtn.Comp != null)
            {
                HideBtn.Comp.SetButtonListener(() => Hide());
            }

            if (BtnHide.Comp != null)
            {
                BtnHide.Comp.SetButtonListener(() => Hide());
            }

            if (BtnBlocker.Comp != null)
            {
                BtnBlocker.Comp.SetButtonListener(() => Hide());
            }
        }

        private void SetVisibleAnimation()
        {
            switch (visibleType)
            {
                case AnimationType.Popping:
                    animation = new PoppingAnimation(this);
                    break;

                case AnimationType.Custom:
                    animation = new CustomAnimation(this);
                    break;

                case AnimationType.CRT:
                    animation = new CRTAnimation(this);
                    break;

                case AnimationType.OpenVertical:
                    animation = new OpenVerticalAnimation(this);
                    break;

                case AnimationType.None:
                default:
                    animation = null;
                    break;
            }
        }

        public void SetVisible(bool visible)
        {
            _canvasGroup.SetVisible(visible);
        }

        public bool GetVisible()
        {
            return _canvasGroup.alpha == 1;
        }

        protected virtual void Show()
        {
            isHiding = false;

            if (animation != null)
            {
                animation.Show();
            }

            SetVisible(true);

            transform.SetAsLastSibling();
        }

        public T Show<T>()
            where T : UIPresenter
        {
            Show();
            return gameObject.AddComponent<T>();
        }

        public UIPresenter Show(Type type)
        {
            Show();
            return (UIPresenter)gameObject.AddComponent(type);
        }

        public virtual void Hide()
        {
            if (isHiding)
                return;

            isHiding = true;

            if (animation != null)
            {
                animation.Hide();

                return;
            }

            Close();
        }

        public void Close()
        {
            OnClosed?.Invoke();
            transform.SetAsFirstSibling();

            SetVisible(false);

            Dispose();

            isHiding = false;
        }

        public void Dispose()
        {
            var disposables = gameObject.GetComponentsInChildren<IDisposable>(true);
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
        }

        public void ReturnRoot()
        {
            transform.parent = UICanvas.Instance.Root;
        }
    }

    public abstract class UIRoot<T> : UIRoot
        where T : UIBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                return _instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (_instance == null)
            {
                _instance = GetComponent<T>();
            }
            else if (_instance != this)
            {
                Debug.LogError("duplicated", gameObject);
                Destroy(gameObject);
            }
        }
    }
}