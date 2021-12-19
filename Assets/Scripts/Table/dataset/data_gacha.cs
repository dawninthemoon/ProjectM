using System;

namespace Data
{
    [Serializable]
    public partial class GachaGameData : GameData<GachaGameData>
    {
        public int key { get; set; }

        public string name { get; set; }

        public int sequnce { get; set; }

        public CurrencyType cashType { get; set; }
        public int cashValue { get; set; }

        public string couponKey { get; set; }
        public int couponMileageStack { get; set; }

        public int randomBoxKey { get; set; }

        public int mileageKey { get; set; }
        public int mileageValue { get; set; }

        public int advantageMileageValue { get; set; }
        //public int advantageRandomBoxKey { get; set; }

        public int freeGachaType { get; set; }
        public int freeGachaValue { get; set; }

        public int GetKey()
        {
            return key;
        }
    }
}