using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace OutGame
{
    public class UILobbyView : UIRoot<UILobbyView>
    {
        public UIElem<RectTransform> BtnQuest { get; } = new 
            UIElem<RectTransform>();
        public UIElem<RectTransform> BtnDeck { get; } = new
            UIElem<RectTransform>();
        public UIElem<RectTransform> BtnRade { get; } = new
            UIElem<RectTransform>();
        public UIElem<RectTransform> BtnDungeon { get; } = new
            UIElem<RectTransform>();


        protected override void Awake()
        {
            base.Awake();
        }
    }
}