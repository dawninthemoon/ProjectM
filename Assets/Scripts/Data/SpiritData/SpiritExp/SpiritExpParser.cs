using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class SpiritExpParser : PublicDataParseBase<SpiritExpParser, SpiritExpData>
    {
        public SpiritExpParser()
        {
            assetPath = "Json/spiritExp";
            templateName = "SpiritExpTemplate";
        }
    }
}