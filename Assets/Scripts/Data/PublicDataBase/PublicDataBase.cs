using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data
{
    public abstract class PublicDataBase
    {
        public abstract void Parse( JSONObject jsonObject );
    }
}