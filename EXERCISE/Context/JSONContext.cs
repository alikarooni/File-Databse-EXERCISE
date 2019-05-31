using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace EXERCISE.Classes
{
    public class JSONContext : IContext
    {
        public string FilePath = AppDomain.CurrentDomain.BaseDirectory + "file.txt";

        public Record CreateNewRecord(int ID = 0)
        {
            if (ID == 0)
                ID = GenerateNewID();

            Record rec = new Record(ID);

            if (!File.Exists(FilePath))
            {
                List<Record> list = new List<Record>();
                list.Add(rec);

                File.WriteAllText(FilePath, JsonConvert.SerializeObject(list));
            }
            else
            {
                string file = File.ReadAllText(FilePath);

                List<Record> list = JsonConvert.DeserializeObject<List<Record>>(file);
                list.Add(rec);

                File.Delete(FilePath);

                File.WriteAllText(FilePath, JsonConvert.SerializeObject(list));
            }

            return rec;
        }

        public List<Record> Search(byte Type, ushort SubType)
        {
            if (!File.Exists(FilePath))
                return null;

            List<Record> result = new List<Record>();
            
            string file = File.ReadAllText(FilePath);
            List<Record> list = JsonConvert.DeserializeObject<List<Record>>(file);

            foreach (var item in list)
            {
                if (item.Type == Type || item.Subtype == SubType)
                    result.Add(item);
            }

            return result;
        }

        public int GenerateNewID()
        {
            if (!File.Exists(FilePath))
                return 1;

            string file = File.ReadAllText(FilePath);
            List<Record> list = JsonConvert.DeserializeObject<List<Record>>(file);

            int max = list.Max(x => x.ID);

            return max + 1;
        }

        public void Initialize()
        {
            if (File.Exists(FilePath))
                return;

            for (int i = 0; i < 100; i++)
                CreateNewRecord(GenerateNewID());
        }
    }
}
