using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class LobbyPage : MonoBehaviour
    {
        [SerializeField] private string key;
        public string Key
        {
            get { return key; }
        }

        [SerializeField] private GameObject[] pageElement;

        public void SetActive(bool isActive)
        {
            for (int i = 0; i < pageElement.Length; ++i)
            {
                pageElement[i].gameObject.SetActive(isActive);
            }
        }
    }
}