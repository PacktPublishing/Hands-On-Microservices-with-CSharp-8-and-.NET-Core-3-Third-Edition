using System;

namespace NewFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tNew features of C#8.0");
            string result = new String('-', 25); 
            Console.WriteLine($"\t{result}\n");
            IndicesRanges();
            Console.ReadKey();
        }

        private static void IndicesRanges()
        {
            string[] Books = new string[] { "Microservices", "Design Patterns", "Test Driven Development", "Data Sciences", "Functional Programming", "Concurrent Programming" };
            Console.WriteLine("\t{0,-25}{1,-20}{2,-20}", "Name", "index from start", "index from end");
            string result = new String('-', 65);
            Console.WriteLine($"\t{result}\n");
            for (int index = 0; index < Books.Length; index++)
            {
                Console.WriteLine("\t{0,-25}{1,-20}{2,-20}", Books[index], index, Books.Length - index);
               // Console.WriteLine($"\t{Books[index]}\t\t\t\t{index}\t\t{Books.Length - index}");

            }
        }
    }
}
