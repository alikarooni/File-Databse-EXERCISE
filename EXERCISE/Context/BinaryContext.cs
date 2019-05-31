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
    public class BinaryContext : IContext
    {
        public string FilePath = AppDomain.CurrentDomain.BaseDirectory + "file2.bin";

        public Record CreateNewRecord(int ID = 0)
        {
            if (ID == 0)
                ID = GenerateNewID();

            Record rec = new Record(ID);

            if (!File.Exists(FilePath))
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create, FileAccess.Write)))
                {
                    //keeping Maminum ID in the beginning of the file
                    writer.Write(rec.ID);

                    writer.Write(rec.ID);
                    writer.Write(rec.Type);
                    writer.Write(rec.Subtype);
                    writer.Write(rec.Data);
                }
            }
            else
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Append, FileAccess.Write)))
                {
                    writer.Write(rec.ID);
                    writer.Write(rec.Type);
                    writer.Write(rec.Subtype);
                    writer.Write(rec.Data);
                }
            }

            return rec;
        }

        public List<Record> Search(byte Type, ushort SubType)
        {
            List<Record> list = new List<Record>();
            Record rec;

            using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //skip first ID of the file. 
                reader.ReadInt32();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    rec = new Record()
                    {
                        ID = reader.ReadInt32(),
                        Type = reader.ReadByte(),
                        Subtype = reader.ReadUInt16(),
                        Data = reader.ReadString()
                    };

                    if (rec.Type == Type || rec.Subtype == SubType)
                        list.Add(rec);
                }
            }

            return list;
        }

        public int GenerateNewID()
        {
            // instead of iterating through whole the file, we can just increase ID in the beginning part of the file

            if (!File.Exists(FilePath))
                return 1;

            int max;

            using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                max = reader.ReadInt32();
            }

            max += 1;

            using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Open, FileAccess.Write)))
            {
                writer.Write(max);
            }

            return max;
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
