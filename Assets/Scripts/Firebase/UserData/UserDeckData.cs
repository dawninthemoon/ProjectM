using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDeckData
{
    private int[] charIndies = new int[]{ 101, 102, 103 };
    public int[] CharIndies
    {
        get{ return charIndies; }
    }
    private int[] spiritIndies = new int[9];
    private int[] subSpiritIndies;

    public void SetCharacterIndex( int index, int characterIndex )
    {
        charIndies[index] = characterIndex;
    }

    public void SetMainSpiritIndex( int index, int spirit )
    {
        spiritIndies[index] = spirit;       
    }

    public void SetSubSpiritIndex( int index, int spirit )
    {
        subSpiritIndies[index] = spirit;
    }
    public int GetMainSpiritIndex( int index )
    {
        return spiritIndies[index]; 
    }
    public int GetSubSpiritIndex( int index )
    {
        return subSpiritIndies[index]; 
    }
}