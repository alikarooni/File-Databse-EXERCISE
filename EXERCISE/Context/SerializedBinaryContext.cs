using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

namespace EXERCISE.Classes
{
    public class SerializedBinaryContext : IContext
    {
        public string FilePath = AppDomain.CurrentDomain.BaseDirectory + "file.bin";

        public Record CreateNewRecord(int ID = 0)
        {
            if (ID == 0)
                ID = GenerateNewID();

            Record rec = new Record(ID);

            if (!File.Exists(FilePath))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                formatter.Serialize(stream, rec);

                stream.Close();
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(FilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                formatter.Serialize(stream, rec);

                stream.Close();
            }

            return rec;
        }

        public List<Record> Search(byte Type, ushort SubType)
        {
            List<Record> list = new List<Record>();
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            while (stream.Position < stream.Length)
            {
                Record rec = (Record)formatter.Deserialize(stream);

                if (rec.Type == Type || rec.Subtype == SubType)
                    list.Add(rec);
            }

            stream.Close();

            return list;
        }

        public int GenerateNewID()
        {
            if (!File.Exists(FilePath))
                return 1;

            int max = -1;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            while (stream.Position < stream.Length)
            {
                Record rec = (Record)formatter.Deserialize(stream);

                if (rec.ID > max)
                    max = rec.ID;
            }

            stream.Close();

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
