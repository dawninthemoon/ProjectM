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
        private System.Action<UserSpiritData> onClickItemCallback;

        public void SetItemClickCallback(System.Action<UserSpiritData> onClickItemCallback)
        {
            this.onClickItemCallback = onClickItemCallback;
        }

        public void ViewAllSpirit()
        {
            View.SpiritPool.Clear();
            UserSpiritData[] userSpiritDatas = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.ToArray();

            System.Array.Sort(userSpiritDatas, (x, y) => { return x.Grade.CompareTo(y.Grade); });

            foreach (var spritElement in userSpiritDatas)
            {
                SpiritGameData spiritGameData = SpiritGameData.Get(spritElement.Index);
                var presenter = View.SpiritPool.Add<UIPromotionItemPresenter>();
                presenter.SetArgs((spiritGameData, spritElement, onClickItemCallback));
                presenter.View.UISpiritListItem.ActiveButton();
            }
        }

        public void ViewSameGradeSpirit(int grade)
        {
            View.SpiritPool.Clear();
            int targetGrade = grade;

            UserSpiritData[] userSpiritDatas = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.ToArray();
            System.Array.Sort(userSpiritDatas, (x, y) => { return x.Grade.CompareTo(y.Grade); });

            UserSpiritData[] datas = System.Array.FindAll(userSpiritDatas, (x) => { return x.Grade == grade; });

            foreach (var spritElement in datas)
            {
                SpiritGameData spiritGameData = SpiritGameData.Get(spritElement.Index);
                
                var presenter = View.SpiritPool.Add<UIPromotionItemPresenter>();
                presenter.SetArgs((spiritGameData, spritElement, onClickItemCallback));
                presenter.View.UISpiritListItem.ActiveButton();
            }
        }
    }
}