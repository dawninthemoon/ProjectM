using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Boomlagoon.JSON;

namespace Data {
    public class MonsterStatDataParser : PublicDataParseBase<MonsterStatDataParser, MonsterStat> {
        public MonsterStatDataParser() {
            assetPath = "Json/monsterStat";
            templateName = "MonsterStatTemplate";
        }

        public MonsterStat GetMonsterStat(int key)
        {
            return Array.Find(data, (x) => x.Key == key );
        }
    }

    public class MonsterDataParser : PublicDataParseBase<MonsterDataParser, Monster> {

        public MonsterDataParser() {
            assetPath = "Json/monster";
            templateName = "MonsterTemplate";
        }

        public Monster GetMonster(int key)
        {
            return Array.Find(data, (x) => x.Key == key );
        }
    }
}
