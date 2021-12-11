using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class UIPresenter : MonoBehaviour, IDisposable
    {
        protected virtual void Awake()
        {
        }

        public virtual void Dispose()
        {
            Destroy(this);
        }
    }

    public abstract class UIPresenter<TView> : UIPresenter
    {
        public TView View { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            View = GetComponent<TView>();
        }
    }

    public abstract class UIPresenterWithoutArgs<TView>
        : UIPresenter<TView>
        where TView : UIBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            Bind();
        }

        protected abstract void Bind();
    }

    public abstract class UIPresenterWithArgs<TView, TArgs>
        : UIPresenter<TView>
        where TView : UIBehaviour
    {
        public TArgs Args { get; private set; }

        public void SetArgs(TArgs args)
        {
            Args = args;
            Bind();
        }

        private void Bind()
        {
            if (Args == null)
            {
                BindNull();
            }
            else
            {
                Bind(Args);
            }
        }

        protected abstract void Bind(TArgs args);

        protected virtual void BindNull()
        {
            throw new NotImplementedException();
        }
    }
}