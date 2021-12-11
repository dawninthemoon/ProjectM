using FirebaseManager = FBControl.FirebaseManager;

public enum CurrencyType
{
    FreeCash,
    PaidCash,
    Gold,
    Cash
}

public class UserCurrenyData
{
    private int gold; //InGame Curreny

    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;

            if (gold < 0)
                gold = 0;

            OnChangeCurrenyEvent?.Invoke(CurrencyType.Gold);
            SetCurrencyDB();
        }
    }

    private int fCash; //freeCash

    public int FCash
    {
        get { return fCash; }
        set
        {
            fCash = value;

            if (fCash < 0)
                fCash = 0;

            OnChangeCurrenyEvent?.Invoke(CurrencyType.FreeCash);
            SetCurrencyDB();
        }
    }

    private int pCash; //PaidCash

    public int PCash
    {
        get { return pCash; }
        set
        {
            pCash = value;

            if (pCash < 0)
                pCash = 0;

            OnChangeCurrenyEvent?.Invoke(CurrencyType.PaidCash);
            SetCurrencyDB();
        }
    }

    public event System.Action<CurrencyType> OnChangeCurrenyEvent;

    public void SetCurrencyDB()
    {
        FirebaseManager.Instance.UserDB.SaveChildrenData(gold, "gold");
        FirebaseManager.Instance.UserDB.SaveChildrenData(fCash, "fCash");
        FirebaseManager.Instance.UserDB.SaveChildrenData(pCash, "pCash");
    }
}