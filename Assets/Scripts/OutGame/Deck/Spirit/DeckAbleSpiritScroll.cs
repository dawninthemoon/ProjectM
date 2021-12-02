using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
using PolyAndCode.UI;

namespace OutGame
{
    public class DeckAbleSpiritScroll : DeckScrollBase, IRecyclableScrollRectDataSource
    {
        [SerializeField] private RecyclableScrollRect recyclableScrollRect;
        private UserSpiritData[] userSpiritDatas = null;


        public void Awake()
        {
            Init();
        }

        public override void Init()
        {
            userSpiritDatas = FirebaseManager.Instance.UserData.UserSpiritDataList.Data.ToArray(); 
            recyclableScrollRect.Initialize( this );
        
            Debug.Log( userSpiritDatas.Length  + "ELLEG");
        }
        
        public int GetItemCount()
        {
            if( userSpiritDatas != null)
                return userSpiritDatas.Length;
            else
                return 0;
        }

        public void SetCell(ICell cell, int index)
        {
            DeckAbleSpirit deckElement = cell as DeckAbleSpirit;

            deckElement.SetIndex( userSpiritDatas[index].Index, OnSelectCallback );
        }
    }
}