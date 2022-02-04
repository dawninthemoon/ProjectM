using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class LobbyPageControl : MonoBehaviour
    {
        [SerializeField] private LobbyPage[] lobbyPages;

        public void Start()
        {
            UISpiritListView.Instance.Show<UISpiritListViewPresenter>();
            
        }

        public void SetElement(string key)
        {
            int elementIndex = System.Array.FindIndex(lobbyPages, (x) => { return x.Key.Contains(key); });

            if (elementIndex == -1)
                return;

            for( int i =0; i <lobbyPages.Length; ++i )
            {
                lobbyPages[i].SetActive(i == elementIndex);
            }
        }
    }
}