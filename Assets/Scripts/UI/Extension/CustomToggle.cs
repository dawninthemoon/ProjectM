using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : Toggle
{
    [SerializeField] private GameObject _onObject;
    [SerializeField] private GameObject _offObject;

    protected override void Start()
    {
        base.Start();

        OnValueChanged(isOn);
        onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool isToggleOn)
    {
        if (_onObject)
            _onObject.SetActive(isToggleOn);

        if (_offObject)
            _offObject.SetActive(!isToggleOn);
    }

    public void RemoveAllListeners()
    {
        onValueChanged.RemoveAllListeners();
        onValueChanged.AddListener(OnValueChanged);
    }
}