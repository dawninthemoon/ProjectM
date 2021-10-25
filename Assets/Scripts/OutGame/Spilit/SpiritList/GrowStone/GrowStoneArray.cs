using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class GrowStoneArray : MonoBehaviour
    {
        [SerializeField] private GrowStoneElement[] growStoneElements;
        [SerializeField] private GrowStoneButton[] growStoneButtons;

        private int[] growStone = new int[ UserGrowStoneData.GROW_STONE_COUNT ];

        public void Start()
        {
            for( int i = 0; i < growStoneButtons.Length; ++i )
            {
                growStoneButtons[i].Init( this, i );
            }
        }

        public void AddStone( int index, int count )
        {
            growStone[index] += count;
        
            growStoneElements[index].SetCountText( growStone[index] );
        }
    }
}