using Boomlagoon.JSON;

namespace Data
{
    public class BattleStageData : PublicDataBase
    {
        public string Key
        {
            get;
            private set;
        }

        public string Round1_Formation
        {
            get;
            private set;
        }

        public int[] Round1_Monster
        {
            get;
            private set;
        }

        public string Round2_Formation
        {
            get;
            private set;
        }

        public int[] Round2_Monster
        {
            get;
            private set;
        }

        public string Round3_Formation
        {
            get;
            private set;
        }

        public int[] Round3_Monster
        {
            get;
            private set;
        }

        public string Round4_Formation
        {
            get;
            private set;
        }

        public int[] Round4_Monster
        {
            get;
            private set;
        }

        public string Round5_Formation
        {
            get;
            private set;
        }

        public int[] Round5_Monster
        {
            get;
            private set;
        }

        public override void Parse(JSONObject jsonObject)
        {
            Key = jsonObject.GetString("Key");

            Round1_Formation = jsonObject.GetString("Round1_Formation");
            Round1_Monster = GetMonsterID(jsonObject.GetString("Round1_Monster"));

            Round2_Formation = jsonObject.GetString("Round2_Formation");
            Round2_Monster = GetMonsterID(jsonObject.GetString("Round2_Monster"));

            Round3_Formation = jsonObject.GetString("Round3_Formation");
            Round3_Monster = GetMonsterID(jsonObject.GetString("Round3_Monster"));

            Round4_Formation = jsonObject.GetString("Round4_Formation");
            Round4_Monster = GetMonsterID(jsonObject.GetString("Round4_Monster"));

            Round5_Formation = jsonObject.GetString("Round5_Formation");
            Round5_Monster = GetMonsterID(jsonObject.GetString("Round5_Monster"));
        }

        private int[] GetMonsterID(string origin)
        {
            origin = origin.TrimStart('{');
            origin = origin.TrimEnd('}');
            string[] keys = origin.Split(',');
            int[] nKeys = new int[keys.Length];
            for (int i = 0; i < keys.Length; ++i)
            {
                nKeys[i] = int.Parse(keys[i]);
            }
            return nKeys;
        }
    }
}