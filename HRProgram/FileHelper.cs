using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRProgram
{
    public class FileHelper<T> where T : new()
    {
        private string _filePath;

        public FileHelper(string filePath)
        {
            _filePath = filePath;
        }

        public void SerializeToFile(T workers)
        {
            var serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(_filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, workers);
                writer.Close();
            }
        }

        public T DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new T();

            using (StreamReader reader = File.OpenText(_filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                T workers = (T)serializer.Deserialize(reader, typeof(T));
                reader.Close();
                return workers;
            }
        }
    }
}
