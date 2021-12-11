using Boomlagoon.JSON;

namespace Data
{
    public class GachaData : PublicDataBase
    {
        public int Key;
        public string Name;
        public int Sequnce;
        public CurrencyType CashType;
        public int CashValue;
        public string CouponKey;
        public int CouponMileageStack;
        public int RandomBoxKey;
        public int AdvantageRandomBoxKey;
        public int MileageKey;
        public int MileageValue;
        public int AdvantageMileageValue;
        public int FreeGachaType;
        public int FreeGachaValue;

        public override void Parse(JSONObject jsonObj)
        {
            Key = (int)jsonObj.GetNumber("Key");
            Name = jsonObj.GetString("Name");
            Sequnce = (int)jsonObj.GetNumber("Sequence");
            CashType = (CurrencyType)System.Enum.Parse(typeof(CurrencyType), jsonObj.GetString("CashType"));

            CashValue = (int)jsonObj.GetNumber("CashValue");
            CouponKey = jsonObj.GetString("CouponKey");

            CouponMileageStack = (int)jsonObj.GetNumber("CouponMileageStack");
            RandomBoxKey = (int)jsonObj.GetNumber("RandomBoxKey");
            if (jsonObj.ContainsKey("AdvantageRandomBoxKey"))
                AdvantageRandomBoxKey = (int)jsonObj.GetNumber("AdvantageRandomBoxKey");
            if (jsonObj.ContainsKey("MileageKey"))
                MileageKey = (int)jsonObj.GetNumber("MileageKey");
            if (jsonObj.ContainsKey("MileageValue"))
                MileageValue = (int)jsonObj.GetNumber("MileageValue");
            if (jsonObj.ContainsKey("AdvantageMileageValue"))
                AdvantageMileageValue = (int)jsonObj.GetNumber("AdvantageMileageValue");
            if (jsonObj.ContainsKey("FreeGachaType"))
                FreeGachaType = (int)jsonObj.GetNumber("FreeGachaType");
            if (jsonObj.ContainsKey("FreeGachaValue"))
                FreeGachaValue = (int)jsonObj.GetNumber("FreeGachaValue");
        }
    }
}