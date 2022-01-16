using UnityEngine;
using UnityEngine.Events;
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

        public static void SetButtonListener(this Button button, UnityAction call)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(call);
        }

        public static void SetStars(this RectTransform parent, int count)
        {
            int index = 0;

            foreach (Transform child in parent)
            {
                if (index >= count)
                    child.gameObject.SetActive(false);
                else
                    child.gameObject.SetActive(true);

                index++;
            }
        }
    }
}