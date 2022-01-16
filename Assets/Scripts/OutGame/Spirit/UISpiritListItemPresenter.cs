using UnityEngine;
using UI;
using Data;
using Utills;

namespace OutGame
{
    public class UISpiritListItemPresenter : UIPresenterWithArgs<UISpiritListItem, (SpiritGameData, UserSpiritData)>
    {
        protected override void Bind((SpiritGameData, UserSpiritData) tuple)
        {
            var (spiritData, userSpiritData) = tuple;

            if (userSpiritData == null)
            {
                // TODO : 나중에 대응 UI 생기면 작업 ( hyeonDo )

                return;
            }

            int requestSoul = SpiritData.GetRequestSoulToStar(userSpiritData.Star);
            View.SoulCountFill.Comp.fillAmount = userSpiritData.Soul / (float)requestSoul;
            View.SoulCount.Comp.text = string.Format("{0}/{1}", userSpiritData.Soul, requestSoul);
            View.ParentStar.Comp.SetStars(userSpiritData.Star);
            View.Level.Comp.text = string.Format("LV\n{0}", userSpiritData.Lv);
            View.Icon.Comp.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite(spiritData.key);

            // TODO : 나중에 Spirit Info UI 확정되면 수정하기( hyeonDo )
            //View.BtnSelect.Comp.SetButtonListener
        }
    }
}