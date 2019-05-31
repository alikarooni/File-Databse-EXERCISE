using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXERCISE.Classes
{
    interface IContext
    {
        Record CreateNewRecord(int ID = 0);
        List<Record> Search(byte Type, ushort SubType);
        int GenerateNewID();
        void Initialize();
    }
}
