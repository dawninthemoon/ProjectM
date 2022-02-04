using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UISpiritListViewPresenter : UIPresenterWithoutArgs<UISpiritListView>
    {
        protected override void Bind()
        {
            foreach (var data in SpiritGameData.All)
            {
                var spiritList = FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data;
                UserSpiritData userData = spiritList.Find((x) => { return x.Index == data.key; });

                var presenter = View.SpiritPool.Add<UISpiritListItemPresenter>();
                presenter.SetArgs((data, userData));

                if (userData != null)
                {
                    presenter.transform.parent = View.ParentHave.Comp;
                    presenter.View.ActiveButton();
                }
                else
                {
                    presenter.transform.parent = View.ParentLock.Comp;
                    presenter.View.DisableButton();
                }
            }
        }
    }
}