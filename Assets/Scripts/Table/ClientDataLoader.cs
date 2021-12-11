using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ClientDataLoader
{
    public static T[] Load<T>(string fileName)
    {
        return LoadJson<T>(fileName);

    }

    public static T[] LoadJson<T>(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Tables/json/{fileName}");
        string jsonString = textAsset.text;

        using (var reader = new JsonTextReader(new StringReader(jsonString)))
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    var serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings()
                    {
                        ContractResolver = new DolphinContractResolver(),
                    });

                    return serializer.Deserialize<T[]>(reader);
                }
            }
        }
        return new T[0];
    }
}

internal class DolphinContractResolver : DefaultContractResolver
{
    public class OADateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.FromOADate(Convert.ToDouble(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var list = base.CreateProperties(type, memberSerialization);

        return list.Where(elem => elem.Readable && elem.Writable).ToList();
    }

    protected override JsonContract CreateContract(Type objectType)
    {
        JsonContract contract = base.CreateContract(objectType);
        if (objectType == typeof(DateTime))
        {
            contract.Converter = new OADateConverter();
        }
        return contract;
    }
}