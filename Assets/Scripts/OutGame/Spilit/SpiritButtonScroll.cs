using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using FBControl;

public class SpiritButtonScroll : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField] private RecyclableScrollRect recyclableScrollRect;
    [SerializeField] private SpiritInfoUI spilitInfoUI;

    public void Awake()
    {
        recyclableScrollRect.DataSource = this;
    }

    public int GetItemCount()
    {
        return FirebaseManager.Instance.UserData.UserSpiritDataList.Data.Count;
    }

    public void SetCell( ICell cell, int index )
    {
        //Casting to the implemented Cell
        var item = cell as SpiritButton;
        item.Init( FirebaseManager.Instance.UserData.UserSpiritDataList.Data[index].Index, spilitInfoUI );
    }
}
