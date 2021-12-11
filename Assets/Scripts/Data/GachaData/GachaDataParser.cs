using System;

namespace Data
{
    public class GachaDataParser : PublicDataParseBase<GachaDataParser, GachaData>
    {
        public GachaDataParser()
        {
            assetPath = "Json/gacha";
            templateName = "GachaTemplate";
        }

        public GachaData GetGachaData(int gachaIndex)
        {
            return Array.Find(data, (x) => x.Key == gachaIndex);
        }
    }
}