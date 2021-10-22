using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class StageDataParser : PublicDataParseBase<StageDataParser, StageData>
    {
        public StageDataParser()
        {
            assetPath = "Json/stage";
            templateName = "StageTemplate";
        }
    }
}