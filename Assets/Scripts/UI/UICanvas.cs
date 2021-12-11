using UI;
using UnityEngine;
using Utills;

public class UICanvas : SingletonBehaviour<UICanvas>
{
    [SerializeField] private Transform root;

    public Transform Root => root;

    protected override void OnAwake()
    { }

    private void Start()
    {
        HideAll();
    }

    public void HideAll()
    {
        foreach (var child in root.GetComponentsInChildren<UIRoot>())
        {
            child.SetVisible(false);
        }
    }
}