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
        private string subName;
        private string thumbnail;
        private string firstRewardType;
        private int firstRewarCount;

        public override void Parse(JSONObject jsonObject)
        {
            stage = (int)jsonObject.GetNumber("stage");
            subStage = (int)jsonObject.GetNumber("subStage");
            name = jsonObject.GetString("Name");
            subName = jsonObject.GetString("subName");
            thumbnail = jsonObject.GetString("thumbnail");
            firstRewardType = jsonObject.GetString("firstRewardType");
            firstRewarCount = (int)jsonObject.GetNumber("firstRewarCount");
        }
    }
}