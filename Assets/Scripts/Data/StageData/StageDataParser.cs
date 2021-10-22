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

        public StageData[] GetStageData( int stage )
        {
            return System.Array.FindAll( data, (x) => { return x.Stage == stage; } );
        }
    }
}