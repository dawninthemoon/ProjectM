using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDeckData
{
    private int charIndex;
    public int CharIndex
    {
        get{ return charIndex; }
    }
    private int[] spiritIndies;
    private int[] subSpiritIndies;

    public void SetCharacterIndex( int characterIndex )
    {
        charIndex = characterIndex;
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