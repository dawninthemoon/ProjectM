using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UIPromotionPresenter : UIPresenterWithoutArgs<UIPromotionView>
    {
        protected override void Bind()
        {
            View.PromotionSpiritListPrecenter.Comp.SetItemClickCallback(SetTargetSpirit);
            View.PromotionSpiritListPrecenter.Comp.ViewAllSpirit();
        }

        public void SetTargetSpirit(int key)
        {
            View.SetSpirit(key);
        }
    }
}