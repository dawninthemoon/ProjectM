using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using FBControl;
using UnityEngine.UI;
using Data;

public class SpiritButtonScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private SpiritInfoUI spilitInfoUI;

    [SerializeField] private Transform haveTransform;
    [SerializeField] private Transform lockTransform;

    [SerializeField] private SpiritButton[] spiritButtonPool;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        SpiritData[] spiritDatas = SpiritDataParser.Instance.Data;
        List<UserSpiritData> userSpiritData = FirebaseManager.Instance.UserData.UserSpiritDataList.Data;

        for( int i =0; i < spiritDatas.Length; ++i )
        {
            SpiritButton button = Pool();

            if( button == null )
                return;
                
            button.gameObject.SetActive( true );
            button.Init( spiritDatas[i].Key, spilitInfoUI );

            if( userSpiritData.Find( (x)=>{ return x.Index == spiritDatas[i].Key;} ) != null )
            {
                button.transform.parent = haveTransform;
            }   
            else
            {
                button.transform.parent = lockTransform;
            } 
        }
    }

    public SpiritButton Pool()
    {
        for( int i =0 ; i <spiritButtonPool.Length; ++i )
        {
            if( !spiritButtonPool[i].gameObject.activeSelf )
            {
                return spiritButtonPool[i];
            }
        }

        return null;
    }
}
