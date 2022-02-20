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
                // TODO : 나중에 대응 UI 생기면 작업 ( hyeonDo )

                return;
            }
            int requestSoul = SpiritData.GetRequestSoulToStar(userSpiritData.Star);
            View.UISpiritListItem.SetInfo(userSpiritData.Lv, spiritData.key);
            View.Button.Comp.onClick.AddListener(OnClick);

            spiritKey = spiritData.key;

            // TODO : 나중에 Spirit Info UI 확정되면 수정하기( hyeonDo )
            //View.BtnSelect.Comp.SetButtonListener
        }

        public void OnClick()
        {
            onClickCallback?.Invoke(spiritKey);
        }
    }
}