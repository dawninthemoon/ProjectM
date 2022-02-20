using UnityEngine;
using UI;
using Data;
using Utills;


namespace OutGame
{
    public class UIPromotionItemPresenter : UIPresenterWithArgs<UIPromotionItem, (SpiritGameData, UserSpiritData, System.Action<int>)>
    {
        private int spiritKey = 0;
        private System.Action<int> onClickCallback;
        protected override void Bind((SpiritGameData, UserSpiritData, System.Action<int>) tuple)
        {
            var (spiritData, userSpiritData, onClick) = tuple;
            onClickCallback = onClick;
            if (userSpiritData == null)
            {
                // TODO : ���߿� ���� UI ����� �۾� ( hyeonDo )

                return;
            }
            int requestSoul = SpiritData.GetRequestSoulToStar(userSpiritData.Star);
            View.UISpiritListItem.SetInfo(userSpiritData.Lv, spiritData.key);
            View.Button.Comp.onClick.AddListener(OnClick);

            spiritKey = spiritData.key;

            // TODO : ���߿� Spirit Info UI Ȯ���Ǹ� �����ϱ�( hyeonDo )
            //View.BtnSelect.Comp.SetButtonListener
        }

        public void OnClick()
        {
            onClickCallback?.Invoke(spiritKey);
        }
    }
}