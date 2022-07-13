using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sdcTool
{
    public class JsonHelper
    {
        public static string SerializeObjct(object obj) => JsonConvert.SerializeObject(obj);

        public static T JsonConvertObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public static T DeserializeJsonToObject<T>(string json) where T : class => new JsonSerializer().Deserialize((JsonReader)new JsonTextReader((TextReader)new StringReader(json)), typeof(T)) as T;

        public static List<T> DeserializeJsonToList<T>(string json) where T : class => new JsonSerializer().Deserialize((JsonReader)new JsonTextReader((TextReader)new StringReader(json)), typeof(List<T>)) as List<T>;

        public static JArray GetToJsonList(string json) => (JArray)JsonConvert.DeserializeObject(json);

        public static List<T> DtConvertToModel<T>(DataTable dt) where T : new()
        {
            List<T> model = new List<T>();
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                T obj1 = new T();
                foreach (PropertyInfo property in obj1.GetType().GetProperties())
                {
                    if (dt.Columns.Contains(property.Name) && property.CanWrite)
                    {
                        object obj2 = row[property.Name];
                        if (obj2 != DBNull.Value)
                        {
                            switch (property.PropertyType.FullName)
                            {
                                case "System.Decimal":
                                    property.SetValue((object)obj1, (object)Decimal.Parse(obj2.ToString()), (object[])null);
                                    break;

                                case "System.String":
                                    property.SetValue((object)obj1, (object)obj2.ToString(), (object[])null);
                                    break;

                                case "System.Int32":
                                    property.SetValue((object)obj1, (object)int.Parse(obj2.ToString()), (object[])null);
                                    break;

                                default:
                                    property.SetValue((object)obj1, obj2, (object[])null);
                                    break;
                            }
                        }
                    }
                }
                model.Add(obj1);
            }
            return model;
        }
    }
}
