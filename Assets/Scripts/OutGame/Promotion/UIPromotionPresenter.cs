using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UIPromotionPresenter : UIPresenterWithoutArgs<UIPromotionView>
    {
        private UserSpiritData targetUserData;
        protected override void Bind()
        {
            View.PromotionSpiritListPrecenter.Comp.SetItemClickCallback(SetTargetSpirit);
            View.PromotionSpiritListPrecenter.Comp.ViewAllSpirit();

            View.ReleaseButton.Comp.onClick.AddListener(ClearTargetSpirit);
        }

        public void ClearTargetSpirit()
        {
            targetUserData = null;
            View.PromotionSpiritListPrecenter.Comp.ViewAllSpirit();
        }

        public void SetTargetSpirit(UserSpiritData userSpiritData)
        {
            targetUserData = userSpiritData;
            View.SetSpirit(userSpiritData.Index);

            PromotionMaterial promotionData
                = System.Array.Find(PromotionInfo.PromotionMaterials, (x) => { return x.BaseGrade == userSpiritData.Grade; });

            View.PromotionSpiritListPrecenter.Comp.ViewSameGradeSpirit(userSpiritData.Grade);
            View.SetPromotionInfo(promotionData);
        }
    }
}