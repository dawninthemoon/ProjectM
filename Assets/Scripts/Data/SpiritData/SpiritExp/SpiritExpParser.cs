namespace Data
{
    public class SpiritExpParser : PublicDataParseBase<SpiritExpParser, SpiritExpData>
    {
        public SpiritExpParser()
        {
            assetPath = "Json/spiritExp";
            templateName = "SpiritExpTemplate";
        }

        public static int MaxLevel
        {
            get { return 60; }
        }

        public int GetSpiritExpToLV(int lv)
        {
            if (data[lv - 1].Level != lv)
            {
                SpiritExpData returnData = System.Array.Find(data, (x) => { return x.Level == lv; });

                if (returnData == null)
                    return 999999999;

                return returnData.MaxExp;
            }
            else
            {
                return data[lv - 1].MaxExp;
            }
        }
    }
}