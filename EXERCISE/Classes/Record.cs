using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXERCISE.Classes
{
    [Serializable]
    public class Record
    {
        public int ID { get; set; }
        public byte Type { get; set; }
        public ushort Subtype { get; set; }
        public string Data { get; set; }

        public Record()
        { }

        public Record(int ID)
        {
            this.ID = ID;

            Type = StoredProcedures.CreateRandomByte();
            Subtype = StoredProcedures.CreateRandomUint();
            Data = StoredProcedures.CreateRandomString();
        }
    }
}
