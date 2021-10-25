using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using UnityEngine;

namespace Data
{
    public class SpiritExpData : PublicDataBase
    {
        private int grade;
        private int level;
        private int maxExp;
        
        public override void Parse(JSONObject jsonObject)
        {
            grade = (int)jsonObject.GetNumber("Grade");
            level = (int)jsonObject.GetNumber("Level");
            maxExp = (int)jsonObject.GetNumber("MaxExp");
        }
    }
}