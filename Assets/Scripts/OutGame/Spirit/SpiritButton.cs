using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class SpiritButton : MonoBehaviour
    {
        private Data.SpiritData spiritData;
        private UserSpiritData userSpiritData;
        [SerializeField] private SpiritIcon spilitIcon;
        private SpiritInfoUI spilitInfoUI;

        [SerializeField] private Image disableDim;

        [Header("InfoUI")]
        [SerializeField] private Image backImage;

        [SerializeField] private TextMeshProUGUI lvText;

        public static readonly Color[] CLASS_COLORES
        = new Color[]
        {
            new Color( 206 / 255f, 126 / 255f, 0 ),
            Color.gray,
            new Color( 41 / 255f, 134 / 255f, 204 / 255f ),
            new Color( 103 / 255f, 78 / 255f, 167 / 255f ),
            new Color( 241 / 255f, 194 / 255f, 50 / 255f )
        };

        [Header("SoulInfoUI")]
        [SerializeField] private Image soulFillSlider;

        [SerializeField] private TextMeshProUGUI soulCountText;
        [SerializeField] private OutGame.Stars stars;

        public void Init(SpiritInfoUI spilitInfoUI, Data.SpiritData spiritData, UserSpiritData userSpiritData)
        {
            this.spiritData = spiritData;
            this.userSpiritData = userSpiritData;

            spilitIcon.SetSpirit(spiritData);

            this.spilitInfoUI = spilitInfoUI;

            backImage.color = CLASS_COLORES[spiritData.Grade - 1];

            SetUserInfo();
        }

        public void ActiveButton()
        {
            disableDim.gameObject.SetActive(false);
            lvText.gameObject.SetActive(true);
            stars.gameObject.SetActive(true);
        }

        public void DisableButton()
        {
            disableDim.gameObject.SetActive(true);
            lvText.gameObject.SetActive(false);
            stars.gameObject.SetActive(false);
        }

        public void SetUserInfo()
        {
            if (userSpiritData == null)
            {
                return;
            }

            int requestSoul = Data.SpiritData.GetRequestSoulToStar(userSpiritData.Star);
            soulFillSlider.fillAmount = userSpiritData.Soul / (float)requestSoul;
            soulCountText.text = string.Format("{0}/{1}", userSpiritData.Soul, requestSoul);
            stars.SetStar(userSpiritData.Star);

            lvText.text = string.Format("LV\n{0}", userSpiritData.Lv);
        }

        public void OnClick()
        {
            spilitInfoUI.SetActive();
            spilitInfoUI.SetInfo(userSpiritData, spiritData);
        }
    }
}