using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using UnityEngine;

namespace Data
{
    public class StageData : PublicDataBase
    {
        private int stage;
        public int Stage
        {
            get{ return stage; }
        }
        private int subStage;
        public int SubStage
        {
            get{ return subStage; }
        }
        private string name;
        public string Name
        {
            get { return name; }
        }
        private string subName;
        public string SubName
        {
            get { return subName; }
        }
        private string thumbnail;
        private string firstRewardType;
        private int firstRewarCount;

        public override void Parse(JSONObject jsonObject)
        {
            stage = (int)jsonObject.GetNumber("Stage");
            subStage = (int)jsonObject.GetNumber("SubStage");
            name = jsonObject.GetString("Name");
            subName = jsonObject.GetString("SubName");
            thumbnail = jsonObject.GetString("Thumbnail");
            firstRewardType = jsonObject.GetString("FirstRewardType");
            firstRewarCount = (int)jsonObject.GetNumber("FirstRewardCount");
        }
    }
}