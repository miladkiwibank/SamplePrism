using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public static class EntityCollectionSortManager
    {
        private static Dictionary<string, bool> m_items = new Dictionary<string, bool>();
        private static string m_fileName;

        public static bool GetOrderByDesc<T>()
        {
            var typeName = (typeof(T)).Name;
            if (!m_items.ContainsKey(typeName))
                return false;
            return m_items[typeName];
        }

        public static void SetOrderByDesc<T>(bool value)
        {
            var typeName = (typeof(T)).Name;
            if (!m_items.ContainsKey(typeName))
                m_items.Add(typeName, value);
            else
                m_items[typeName] = value;
            Save();
        }

        public static void Save()
        {
            if (string.IsNullOrEmpty(m_fileName)) return;
            var text = JsonConvert.SerializeObject(m_items);
            File.WriteAllText(m_fileName, text);
        }

        public static void Load(string fileName)
        {
            m_fileName = fileName;
            try
            {
                var text = File.ReadAllText(m_fileName);
                m_items = JsonConvert.DeserializeObject<Dictionary<string, bool>>(text);
            }
            catch (Exception)
            {
                m_items = new Dictionary<string, bool>();
            }
        }
    }
}
