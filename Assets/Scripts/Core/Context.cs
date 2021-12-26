using Table;
using UnityEngine;
using Utills;

namespace Core
{
    public class Context : SingletonBehaviour<Context>
    {
        protected override void OnAwake()
        {
            TableContainer.This.Initialize();
        }
    }
}
