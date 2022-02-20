using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UISpiritListViewPresenter : UIPresenterWithoutArgs<UISpiritListView>
    {
        protected override void Bind()
        {
            UserSpiritData[] userSpiritDatas = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.ToArray();

            System.Array.Sort(userSpiritDatas, (x, y) => { return x.Grade.CompareTo(y.Grade); });
               
            foreach(var spritElement in userSpiritDatas)
            {
                SpiritGameData spiritGameData = SpiritGameData.Get(spritElement.Index);
                var presenter = View.SpiritPool.Add<UISpiritListItemPresenter>();
                presenter.SetArgs((spiritGameData, spritElement));
                presenter.View.ActiveButton();
            }
        }
    }
}