using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using UnityEngine.UI;

namespace OutGame
{
    public class UILobbyPageButtonView : UIRoot<UILobbyPageButtonView>
    {
        public UIElem<Button> BtnLobby { get; } = new UIElem<Button>();
        public UIElem<Button> BtnSpiritList { get; } = new UIElem<Button>();
        public UIElem<Button> BtnShop { get; } = new UIElem<Button>();

        public void Start()
        {
        }

    }
}