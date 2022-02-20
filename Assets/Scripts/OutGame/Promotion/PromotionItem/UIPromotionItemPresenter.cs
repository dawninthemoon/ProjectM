using UnityEngine;
using UI;
using Data;
using Utills;


namespace OutGame
{
    public class UIPromotionItemPresenter : UIPresenterWithArgs<UIPromotionItem, (SpiritGameData, UserSpiritData, System.Action<UserSpiritData>)>
    {
        private UserSpiritData userSpiritData;
        private System.Action<UserSpiritData> onClickCallback;
        protected override void Bind((SpiritGameData, UserSpiritData, System.Action<UserSpiritData>) tuple)
        {
            var (spiritData, userSpiritData, onClick) = tuple;
            onClickCallback = onClick;
            if (userSpiritData == null)
            {
                // TODO : 나중에 대응 UI 생기면 작업 ( hyeonDo )

                return;
            }
            int requestSoul = SpiritData.GetRequestSoulToStar(userSpiritData.Grade);
            View.UISpiritListItem.SetInfo(userSpiritData);
            View.Button.Comp.onClick.RemoveAllListeners();
            View.Button.Comp.onClick.AddListener(OnClick);

            this.userSpiritData = userSpiritData;

            // TODO : 나중에 Spirit Info UI 확정되면 수정하기( hyeonDo )
            //View.BtnSelect.Comp.SetButtonListener
        }

        public void OnClick()
        {
            onClickCallback?.Invoke(userSpiritData);
        }
    }
}