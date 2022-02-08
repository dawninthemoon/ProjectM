using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace OutGame
{
    public class UILobbyPageButtonPresenter : UIPresenterWithoutArgs<UILobbyPageButtonView>
    {
        private UILobbyViewPresenter uiLobbyViewPresenter;
        private UISpiritListViewPresenter uiSpiritListViewPresenter;

        protected override void Bind()
        {
            View.BtnLobby.Comp.onClick.AddListener(() => { uiLobbyViewPresenter = UILobbyView.Instance.Show<UILobbyViewPresenter>(); });
            View.BtnSpiritList.Comp.onClick.AddListener(() => { uiSpiritListViewPresenter = UISpiritListView.Instance.Show<UISpiritListViewPresenter>(); });
        }
    }
}