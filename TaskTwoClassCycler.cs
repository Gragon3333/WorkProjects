using System;
using System.Collections.Generic;
namespace TestClass
{
    class Cycler
    {
        public int loopCount;
        public List<object> objects;
        public void Write()
        {
            int i = loopCount;
            int j = 0;
            while (i>0)
            {
                if (j>objects.Count-1)
                {
                    j = 0;
                    Console.WriteLine(objects[j]);
                    j++;
                }
                else
                {
                    Console.WriteLine(objects[j]);
                    j++;
                }
                i--;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int loops = Convert.ToInt32(Console.ReadLine());
            List<object> listWithObjects = new List<object>();
            listWithObjects.Add(1);
            listWithObjects.Add(5);
            listWithObjects.Add(6);
            Cycler cycle = new Cycler();
            cycle.loopCount = loops;
            cycle.objects = listWithObjects;
            cycle.Write();
        }
    }
}
