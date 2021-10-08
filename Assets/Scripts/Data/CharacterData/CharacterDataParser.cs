using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Boomlagoon.JSON;

namespace Data
{
    public class CharacterStatDataParser : PublicDataParseBase<CharacterStatDataParser, CharacterStat> {
        public CharacterStatDataParser() {
            assetPath = "Json/characterStat";
            templateName = "CharacterStatTemplate";
        }

        public CharacterStat GetCharacterStat(int key) {
            return Array.Find(data, (x) => x.Key == key);
        }
    }

    public class CharacterDataParser : PublicDataParseBase<CharacterDataParser, Character>
    {
        public CharacterDataParser() {
            assetPath = "Json/character";
            templateName = "CharacterTemplate";
        }

        public Character GetCharacter( int key )
        {
            return Array.Find( data, (x) => x.Key == key );
        }
    }
}