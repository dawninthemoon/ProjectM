using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using FBControl;
using UnityEngine.UI;
using Data;

namespace OutGame
{
    public class SpiritButtonScroll : MonoBehaviour
    {
        [SerializeField] private SpiritInfoUI spilitInfoUI;

        [SerializeField] private Transform haveTransform;
        [SerializeField] private Transform lockTransform;

        [SerializeField] private SpiritButton[] spiritButtonPool;

        public void Start()
        {
            Init();

            spilitInfoUI.OnDisableEvent += SetActive;
            spilitInfoUI.OnActiveEvent += SetDisable;
        }

        public void OnDestroy()
        {
            spilitInfoUI.OnDisableEvent -= SetActive;
            spilitInfoUI.OnActiveEvent -= SetDisable;
        }

        public void SetActive()
        {
            gameObject.SetActive( true );
        }

        public void SetDisable()
        {
            gameObject.SetActive( false );
        }

        public void Init()
        {
            SpiritData[] spiritDatas = SpiritDataParser.Instance.Data;
            List<UserSpiritData> userSpiritData = FirebaseManager.Instance.UserData.UserSpiritDataList.Data;

            System.Array.Sort( spiritDatas, (x,y) => { return y.Grade.CompareTo( x.Grade ); } );

            for( int i =0; i < spiritDatas.Length; ++i )
            {
                SpiritButton button = Pool();

                if( button == null )
                    return;

                UserSpiritData targetUserData = userSpiritData.Find( (x)=>{ return x.Index == spiritDatas[i].Key;} );

                button.gameObject.SetActive( true );
                button.Init( spiritDatas[i].Key, spilitInfoUI, spiritDatas[i], targetUserData );

                if( targetUserData != null )
                {
                    button.transform.parent = haveTransform;
                    button.ActiveButton();
                }   
                else
                {
                    button.transform.parent = lockTransform;
                    button.DisableButton();
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
}