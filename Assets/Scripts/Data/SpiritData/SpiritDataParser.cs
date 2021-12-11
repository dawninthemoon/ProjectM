namespace Data
{
    public class SpiritDataParser : PublicDataParseBase<SpiritDataParser, SpiritData>
    {
        public SpiritDataParser()
        {
            assetPath = "Json/spirit";
            templateName = "SpiritTemplate";
        }

        public SpiritData GetSpiritData(int index)
        {
            return System.Array.Find(data, (x) => { return x.Key == index; });
        }

        public string GetSpiritIconName(int index)
        {
            return GetSpiritData(index).IconName;
        }
    }
}