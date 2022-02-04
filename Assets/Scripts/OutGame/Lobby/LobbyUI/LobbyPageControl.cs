using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class LobbyPageControl : MonoBehaviour
    {
        private UILobbyViewPresenter uiLobbyViewPresenter;
        private UISpiritListViewPresenter uiSpiritListViewPresenter;

        public void Start()
        {
            uiLobbyViewPresenter = UILobbyView.Instance.Show<UILobbyViewPresenter>();
            //uiSpiritListViewPresenter = UISpiritListView.Instance.Show<UISpiritListViewPresenter>();
        }
    }
}