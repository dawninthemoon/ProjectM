using UnityEngine;

namespace Utills
{
    public static class ColorUtil
    {
        public static Color ChangeAlpha(this Color color, float alpha)
        {
            var newColor = new Color(color.r, color.g, color.b, alpha);

            return newColor;
        }
    }
}