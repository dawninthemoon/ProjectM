using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data
{
    public class SpiritDataParser : PublicDataParseBase<SpiritDataParser, SpiritData>
    {
        public SpiritDataParser()
        {
            assetPath = "Json/spirit";
            templateName = "SpiritTemplate";
        }
    }
}