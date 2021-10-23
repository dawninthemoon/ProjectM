public class UserStaminaData
{
    private int maxStamina = 50;
    public int MaxStamina
    {
        get { return maxStamina; }
    }
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

    public void AddStamina( int addCount )
    {
        stamina += addCount;

        if (stamina <= 0)
            stamina = 0;

        FBControl.FirebaseManager.Instance.UserDB.SaveChildrenData("stamina", this.stamina);
    }
}
