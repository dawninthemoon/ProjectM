using System.Collections;
using System.Collections.Generic;

namespace Data {
    public class BattleStageDataParser : PublicDataParseBase<BattleStageDataParser, BattleStageData> {
        public BattleStageDataParser() {
            assetPath = "Json/battleStage";
            templateName = "BattleStageTemplate";
        }

        public BattleStageData[] FindAllStage(string key)
        {
            return System.Array.FindAll(data, (x) => { return x.Key.Equals(key); } );
        }

        public BattleStageData FindStage(string key)
        {
            return System.Array.Find(data, (x) => { return x.Key.Equals(key); });
        }
    }
}
