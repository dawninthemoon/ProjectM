using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Data;

namespace Table
{
    public class TableContainer
    {
        public static bool IsInitialized = false;
        private static TableContainer _instance;

        public static TableContainer This
        {
            get
            {
                if (_instance == null)
                    _instance = new TableContainer();

                return _instance;
            }
        }

        public static readonly List<(Type, string)> ClientDataFiles = new List<(Type, string)>
        {
            (typeof(CharacterGameData), "character"),
            (typeof(CharacterStatGameData), "characterStat"),
            (typeof(GachaGameData), "gacha"),
            //(typeof(ItemInfoGameData), "itemInfo"),
            (typeof(MonsterGameData), "monster"),
            (typeof(RandomBoxGameData), "randomBox"),
            (typeof(SkillGameData), "skill"),
            (typeof(SpiritGameData), "spirit"),
            (typeof(SpiritExpGameData), "spiritExp"),
        };

        private void LoadAllData()
        {
            foreach (var pair in ClientDataFiles)
            {
                Type type = pair.Item1;
                string filename = pair.Item2;

                if (type == null)
                    continue;

                var method = type.GetMethod("LoadData", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, null, new[] { typeof(string) }, null);
                if (method == null)
                    throw new InvalidOperationException($"Can't find {type.Name}::LoadData(string).");

                method.Invoke(null, new[] { filename });

                Debug.Log($"{type.Name} table is loaded.");
            }
        }

        public void Initialize()
        {
            if (IsInitialized)
                return;

            LoadAllData();

            IsInitialized = true;
        }
    }
}