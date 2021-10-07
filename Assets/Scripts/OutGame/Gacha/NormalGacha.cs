using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
using Data;

public class NormalGacha : MonoBehaviour
{
    private GachaLogic gachaLogic = new GachaLogic();

    [SerializeField] private GameObject canvas;
    [SerializeField] private GachaResultUI gachaResultUI;

    [SerializeField] private int gachaIndex;

    private GachaData gachaData;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        gachaData = GachaDataParser.Instance.GetGachaData( gachaIndex );
    }

    public void OneceGacha()
    {
        //FirebaseManager.Instance.UserData.UserCurrenyData.PCash -= gachaData.CashValue;
        RandomBoxData randomBoxData = gachaLogic.Gacha( gachaIndex );

        OpenResultUI( new RandomBoxData[]{ randomBoxData } );
    }

    public void TenGacha()
    {
        RandomBoxData[] randomBoxData;

        randomBoxData = gachaLogic.Gacha( gachaData, 10 );

        OpenResultUI( randomBoxData );
    }

    public void OpenResultUI( RandomBoxData[] result )
    {
        string s = "";
        for( int i =0; i <result.Length; ++i )
            s += result[i].RewardKey + "/";

        Debug.Log(s);

        canvas.gameObject.SetActive( true );
        gachaResultUI.gameObject.SetActive( true );

        gachaResultUI.SetUI( result );
    }
}
