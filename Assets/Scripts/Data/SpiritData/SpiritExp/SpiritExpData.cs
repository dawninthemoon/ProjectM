using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using UnityEngine;

namespace Data
{
    public class SpiritExpData : PublicDataBase
    {
        public int Level;
        public int MaxExp;
        
        public override void Parse(JSONObject jsonObject)
        {
            Level = (int)jsonObject.GetNumber("Level");
            MaxExp = (int)jsonObject.GetNumber("MaxExp");
        }
    }
}