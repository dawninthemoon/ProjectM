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
        private bool isBindSpirit = false;

        private UserSpiritData[] userSpiritDataSlot = new UserSpiritData[2];
        private PromotionMaterial promotionMaterial;

        protected override void Bind()
        {
            View.PromotionSpiritListPrecenter.Comp.SetItemClickCallback(OnClickSpiritButton);
            View.PromotionSpiritListPrecenter.Comp.ViewAllSpirit();

            View.ReleaseButton.Comp.onClick.AddListener(ClearTargetSpirit);
            View.PromotionButton.Comp.onClick.AddListener(OnPromotion);
        }

        public void ClearTargetSpirit()
        {
            targetUserData = null;
            View.PromotionSpiritListPrecenter.Comp.ViewAllSpirit();

            isBindSpirit = false;

            for(int i = 0; i < userSpiritDataSlot.Length; ++i)
            {
                userSpiritDataSlot[i] = null;
            }
        }

        public void OnClickSpiritButton(UserSpiritData userSpiritData)
        {
            if(isBindSpirit)
            {
                SetMaterial(userSpiritData);
            }
            else
            {
                SetTargetSpirit(userSpiritData);
            }
        }

        public void SetTargetSpirit(UserSpiritData userSpiritData)
        {
            targetUserData = userSpiritData;
            View.SetSpirit(userSpiritData.Index);

            promotionMaterial
                = System.Array.Find(PromotionInfo.PromotionMaterials, (x) => { return x.BaseGrade == userSpiritData.Grade; });

            if (promotionMaterial.PromotionMaterialType == PromotionInfo.PromotionMaterialType.Same)
            {
                View.PromotionSpiritListPrecenter.Comp.ViewSameGradeIndexSpirit(userSpiritData);
            }
            else
            {
                View.PromotionSpiritListPrecenter.Comp.ViewSameGradeSpirit(userSpiritData);
            }
            View.SetPromotionInfo(promotionMaterial);

            isBindSpirit = true;
        }

        public void SetMaterial(UserSpiritData userSpiritData)
        {
            for (int i = 0; i < userSpiritDataSlot.Length; ++i)
            {
                if (userSpiritDataSlot[i] == null)
                {
                    userSpiritDataSlot[i] = userSpiritData;
                    if(i == 0)
                        View.Material1.Comp.SetMaterial(userSpiritData);
                    else
                        View.Material2.Comp.SetMaterial(userSpiritData);
                    return;
                }
            }
        }

        public void OnPromotion()
        {
            List<UserSpiritData> data = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data;

            int index = data.FindIndex((x) => { return x == targetUserData; });

            data[index].Grade = promotionMaterial.ToGrade;
            FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.SetGrade(index, data[index].Grade);

            ClearTargetSpirit();
        }
    }
}