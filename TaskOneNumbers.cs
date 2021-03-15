using System;
using System.Collections.Generic;
using System.Linq;
namespace Test
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            
            string someText = Console.ReadLine();
            List<char> numbers = new List<char>();
            char[] textToArray = someText.ToCharArray();
            int[] charsToASCII = new int[textToArray.Length];
            for (int i = 0; i < charsToASCII.Length; i++)
            {
                charsToASCII[i] = Convert.ToInt32(textToArray[i]);
            }

            for (int i = 0; i < charsToASCII.Length; i++)
            {
                if (charsToASCII[i] > 47 && charsToASCII[i] < 58)
                {
                    numbers.Add(Convert.ToChar(charsToASCII[i]));
                }
                
            }
            char[] singleNumbers = numbers.Distinct().ToArray();
            foreach (var item in singleNumbers)
            {
                var ct = numbers.Count(cg => cg == item);
                Console.WriteLine(item+":"+ct);
            }
            Console.ReadKey();
        }
    }
}
