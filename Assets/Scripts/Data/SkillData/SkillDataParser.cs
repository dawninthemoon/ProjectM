using System;

namespace Data
{
    public class SkillDataParser : PublicDataParseBase<SkillDataParser, SkillData>
    {
        public SkillDataParser()
        {
            assetPath = "Json/skill";
            templateName = "SkillTemplate";
        }

        public SkillData GetSkillData(int key)
        {
            return Array.Find(data, (x) => x.Key == key);
        }
    }
}