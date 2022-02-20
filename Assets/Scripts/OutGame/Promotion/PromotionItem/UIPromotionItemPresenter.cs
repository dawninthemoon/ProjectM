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
                // TODO : ���߿� ���� UI ����� �۾� ( hyeonDo )

                return;
            }
            int requestSoul = SpiritData.GetRequestSoulToStar(userSpiritData.Grade);
            View.UISpiritListItem.SetInfo(userSpiritData);
            View.Button.Comp.onClick.RemoveAllListeners();
            View.Button.Comp.onClick.AddListener(OnClick);

            this.userSpiritData = userSpiritData;

            // TODO : ���߿� Spirit Info UI Ȯ���Ǹ� �����ϱ�( hyeonDo )
            //View.BtnSelect.Comp.SetButtonListener
        }

        public void OnClick()
        {
            onClickCallback?.Invoke(userSpiritData);
        }
    }
}