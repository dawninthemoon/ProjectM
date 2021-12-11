namespace Data
{
    public class TextDataContainer : PublicDataParseBase<TextDataContainer, TextData>
    {
        public TextDataContainer()
        {
            assetPath = "Json/stage";
            templateName = "StageTemplate";
        }

        public string GetKorean(string key)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i].key.Equals(key))
                {
                    return data[i].korean;
                }
            }

            return string.Format("{0}_NOT_FOUND", key);
        }

        public string GetEnglish(string key)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i].key.Equals(key))
                {
                    return data[i].english;
                }
            }

            return string.Format("{0}_NOT_FOUND", key);
        }

        // public string GetPortugal(string key)
        // {
        //     for( int i = 0; i < data.Length; ++i )
        //     {
        //         if (data[i].key.Equals(key))
        //         {
        //             return data[i].portugal;
        //         }
        //     }

        //     return string.Format("{0}_NOT_FOUND", key);
        // }
        public string GetText(string key)
        {
            // switch(Application.systemLanguage)
            // {
            // case SystemLanguage.Korean:
            return GetKorean(key);

            //     case SystemLanguage.English:
            //         return GetEnglish(key);
            // }

            // return "???";
        }
    }
}