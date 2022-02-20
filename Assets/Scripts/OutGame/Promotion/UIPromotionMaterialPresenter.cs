using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class UIPromotionMaterialPresenter : MonoBehaviour
    {
        [SerializeField] private UIPromotionMaterialView view;
        private PromotionMaterial promotionMaterial;

        public void SetPromotionData(PromotionMaterial promotionMaterial)
        {
            this.promotionMaterial = promotionMaterial;

            view.SetRequset(promotionMaterial);
        }
    }
}