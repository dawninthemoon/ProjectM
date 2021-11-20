using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDeckData
{
    private int charIndex;
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
}