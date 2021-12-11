using UnityEngine;
using UnityEngine.UI;


namespace Utills
{
    public static class CanvasUtill
    {
        public static void SetVisible(this CanvasGroup group, bool visible)
        {
            if (group != null)
            {
                group.alpha = visible ? 1 : 0;
                group.blocksRaycasts = visible;
            }
        }

        public static void SetVisible(this MaskableGraphic graphic, bool visible)
        {
            if (graphic != null)
            {
                graphic.color = graphic.color.ChangeAlpha(visible ? 1f : 0f);
                graphic.raycastTarget = visible;
            }
        }
    }
}