using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EXERCISE.Classes
{
    public static class StoredProcedures
    {
        public static Random rnd = new Random();

        public static byte CreateRandomByte()
        {
            return (byte)rnd.Next(1, 99);
        }

        public static ushort CreateRandomUint()
        {
            return (ushort)rnd.Next(1, 300);
        }

        public static int CreateRandomInt(int Min, int Max)
        {
            return rnd.Next(Min, Max);
        }

        public static string CreateRandomString()
        {
            string str = "Machine learning (ML) is the scientific study of algorithms and statistical models that computer systems use in order to perform a specific task effectively without " +
                "using explicit instructions, relying on patterns and inference instead. It is seen as a subset of artificial intelligence. Machine learning algorithms build a " +
                "mathematical model based on sample data, known as training data, in order to make predictions or decisions without being explicitly programmed to perform the " +
                "task.[1][2]:2 Machine learning algorithms are used in a wide variety of applications, such as email filtering, and computer vision, where it is infeasible to develop " +
                "an algorithm of specific instructions for performing the task. Machine learning is closely related to computational statistics, which focuses on making predictions using " +
                "computers. The study of mathematical optimization delivers methods, theory and application domains to the field of machine learning. Data mining is a field of study within " +
                "machine learning, and focuses on exploratory data analysis through unsupervised learning.[3][4] In its application across business problems, machine learning is also referred " +
                "to as predictive analytics.";

            int min = CreateRandomInt(0, (str.Length - 1) / 2), max = (CreateRandomInt((str.Length - 1) / 2, (str.Length - 1)) - min);

            return str.Substring(min, max);
        }
    }
}
