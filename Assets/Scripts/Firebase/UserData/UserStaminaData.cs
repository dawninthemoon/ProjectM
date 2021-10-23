using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStaminaData
{
    private int maxStamina;

    private int stamina;
    public int Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public UserStaminaData()
    {

    }

    public UserStaminaData( int stamina )
    {
        this.stamina = stamina;
    }
}
