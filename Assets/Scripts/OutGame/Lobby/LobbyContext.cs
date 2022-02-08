using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace OutGame
{
    public class LobbyContext : MonoBehaviour
    {
        private List<UIBehaviour> uiBehaviourList = new List<UIBehaviour>();
        public void Start()
        {
            var a = UILobbyPageButtonView.Instance.Show<UILobbyPageButtonPresenter>();

            
            UILobbyView.Instance.Show<UILobbyViewPresenter>();
        }

        public void ShowView(UIBehaviour uiBehaviour)
        {
            uiBehaviourList.Add(uiBehaviour);
        }
    }
}