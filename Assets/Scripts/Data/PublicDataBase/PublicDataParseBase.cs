using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class PublicDataParseBase<T,D> where T : PublicDataParseBase<T,D>, new()
    {
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

        protected virtual void Init()
        {

        }
    }
}