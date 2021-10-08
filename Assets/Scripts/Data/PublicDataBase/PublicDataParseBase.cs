using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data
{
    public class PublicDataParseBase<T,D> where T : PublicDataParseBase<T,D>, new() where D : PublicDataBase, new()
    {
        protected string assetPath = null;
        protected string templateName = null;
        private static T instnace;
        public static T Instance  
        {  
            get
            {
                if(instnace == null)
                {
                    instnace = new T();
                    instnace.Init();
                }

                return instnace;
            }
        }  

        protected D[] data;
        public D[] Data
        {
            get{ return data; }
        }

        private void Init()
        {
            string textAsset = Resources.Load(assetPath).ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);
            JSONArray jsonArray = jsonObject.GetArray(templateName);

            data = new D[jsonArray.Length];
            for(int i = 0; i < jsonArray.Length; ++i) {
                data[i] = new D();
                data[i].Parse(jsonArray[i].Obj);
            }
        }
    }
}