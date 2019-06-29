using System;

namespace NewFeatures
{
    class Program
    {
        private static readonly int num1=5;
        private static readonly int num2=6;

        static void Main(string[] args)
        {
            //IndicesRanges();
            ReadonlyMembers();
            Console.ReadKey();
        }

        private static void ReadonlyMembers()
        {
            Console.WriteLine("\tReadonly members");
            Console.WriteLine($"\t{Replicate('-', 25)}\n\n");
            Console.WriteLine($"\tAdd {num1} + {num2} = {Add}");
        }

        private static void IndicesRanges()
        {
            Console.WriteLine("\tIndices and Ranges");
            Console.WriteLine($"\t{Replicate('-', 25)}\n\n");
            Console.WriteLine($"\t{Replicate('-', 65)}");
            string[] Books = new string[] { "Microservices", "Design Patterns", "Test Driven Development", "Data Sciences", "Functional Programming", "Concurrent Programming" };
            Console.WriteLine("\t{0,-25}{1,-20}{2,-20}", "Name", "Index from start", "Index from end");
            Console.WriteLine($"\t{Replicate('-', 65)}\n");
            for (int index = 0; index < Books.Length; index++)
            {
                Console.WriteLine("\t{0,-25}{1,-20}{2,-20}",
                    Books[index],
                    index,
                    Books.Length - index);
            }
            Console.WriteLine($"\t{Replicate('-', 65)}\n");
            //Print index 0
            Console.WriteLine($"\tFirst element of array (index from start): Books[0] => {Books[0]}");
            Console.WriteLine($"\tFirst element of array (index from end): Books[^6] => {Books[^6]}");
            //Print using range
            Range book = 1..4;
            var books = Books[book];
            Console.WriteLine($"\n\tElement of array using Range: Books[{book}] => {books}\n");
            foreach (var b in books)
            {
                Console.WriteLine($"\t{b}");
            }

        }

        private static string Replicate(char repl = '-', int length = 5) => new string(repl, length);
        public static int Add => num1 + num2;
    }
}
