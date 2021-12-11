using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
public class ImageMaterialAnimationHelper : MonoBehaviour
{
    [SerializeField] private bool isAnimating = false;

    [Range(0f, 1f)]
    [SerializeField] private float sparkleValue;

    [Range(0f, 1f)]
    [SerializeField] private float shineWidth;

    [Range(-0.5f, 1.5f)]
    [SerializeField] private float shineLocation;

    private Image image;

    private float SparkleValue
    {
        set
        {
            float _sparkleValue = image.material.GetFloat("_Sparkle");
            if (_sparkleValue == value)
                return;

            _sparkleValue = value;

            image.material.SetFloat("_Sparkle", _sparkleValue);
        }

        get
        {
            return image.material.GetFloat("_Sparkle");
        }
    }

    private float ShineWidth
    {
        set
        {
            float _shineWidth = image.material.GetFloat("_ShineWidth");
            if (_shineWidth == value)
                return;

            _shineWidth = value;

            image.material.SetFloat("_ShineWidth", _shineWidth);
        }

        get
        {
            return image.material.GetFloat("_ShineWidth");
        }
    }

    private float ShineLocation
    {
        set
        {
            float _shineLocation = image.material.GetFloat("_ShineLocation");
            if (_shineLocation == value)
                return;

            _shineLocation = value;

            image.material.SetFloat("_ShineLocation", _shineLocation);
        }

        get
        {
            return image.material.GetFloat("_ShineLocation");
        }
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        Init();
    }

    private void Update()
    {
        if (image == null)
            return;

        if (!isAnimating)
            return;

        SparkleValue = sparkleValue;
        ShineWidth = shineWidth;
        ShineLocation = shineLocation;
    }

    public void Init()
    {
        if (image == null)
            return;

        SparkleValue = 0f;
        ShineWidth = 0f;
        ShineLocation = 0f;
    }
}
