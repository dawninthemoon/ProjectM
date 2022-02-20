using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
namespace OutGame
{
    public class UIPromotionMaterialView : UIBehaviour
    {
        [SerializeField] public Image SpiritImage;
        [SerializeField] public Image FrameImage;

        public void SetRequset(PromotionMaterial promotionMaterial)
        {
            FrameImage.color = PromotionInfo.GetGradeColor(promotionMaterial.RequestGrade);
        }
    }
}