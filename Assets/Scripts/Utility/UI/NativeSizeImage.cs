using UnityEngine;
using UnityEngine.UI;

public class NativeSizeImage : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float targetSize;

    public void SetTargetSize(float size)
    {
        targetSize = size;
    }

    public void SetSprite(Sprite sprite)
    {
        targetImage.sprite = sprite;
        SetNativeSize();
    }

    public void SetNativeSize()
    {
        targetImage.SetNativeSize();

        float max = targetImage.rectTransform.sizeDelta.x > targetImage.rectTransform.sizeDelta.y ? targetImage.rectTransform.sizeDelta.x : targetImage.rectTransform.sizeDelta.y;

        float divide = max / targetSize;

        targetImage.rectTransform.sizeDelta /= divide;
    }
}