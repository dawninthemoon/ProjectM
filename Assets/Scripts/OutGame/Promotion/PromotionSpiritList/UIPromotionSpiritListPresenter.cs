using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UIPromotionSpiritListPresenter : MonoBehaviour
    {
        [SerializeField] private UIPromotionSpiritList View;
        private System.Action<int> onClickItemCallback;

        public void SetItemClickCallback(System.Action<int> onClickItemCallback)
        {
            this.onClickItemCallback = onClickItemCallback;
        }

        public void ViewAllSpirit()
        {
            UserSpiritData[] userSpiritDatas = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.ToArray();

            System.Array.Sort(userSpiritDatas, (x, y) => { return x.Star.CompareTo(y.Star); });

            foreach (var spritElement in userSpiritDatas)
            {
                SpiritGameData spiritGameData = SpiritGameData.Get(spritElement.Index);
                var presenter = View.SpiritPool.Add<UIPromotionItemPresenter>();
                presenter.SetArgs((spiritGameData, spritElement, onClickItemCallback));
                presenter.View.UISpiritListItem.ActiveButton();
            }
        }

        public void ViewSameGradeSpirit(SpiritData targetSpirit)
        {
            int targetGrade = targetSpirit.Grade;

            UserSpiritData[] userSpiritDatas = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.ToArray();
            System.Array.Sort(userSpiritDatas, (x, y) => { return x.Star.CompareTo(y.Star); });

            foreach (var spritElement in userSpiritDatas)
            {
                SpiritGameData spiritGameData = SpiritGameData.Get(spritElement.Index);
                
                if (spiritGameData.grade == targetGrade)
                {
                    var presenter = View.SpiritPool.Add<UIPromotionItemPresenter>();
                    presenter.SetArgs((spiritGameData, spritElement, onClickItemCallback));
                    presenter.View.UISpiritListItem.ActiveButton();
                }
            }
        }
    }
}