using System;

namespace FormattableString
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FormattableString DEMO");
            Console.WriteLine("======================");
            Console.WriteLine();
            Console.WriteLine(
                "Testing this line: \"You entered '{input}' (word count: {new Func<string, int>(GetWordCount)})");

            var prompt = "Write some words as 'input': ";
            string input;
            do
            {
                Console.WriteLine();
                Console.Write(prompt);
                input = Console.ReadLine();
                Console.WriteLine();
                //var input = "asdf qwer";

                Run($"You entered '{input}' (word count: {new Func<string, int>(GetWordCount)})");

                prompt = "Write some words as 'input' (or press <enter> to exit): ";
            } while (input.Length > 0);
        }

        private static int GetWordCount(string input)
        {
            return input.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private static void Run(System.FormattableString s)
        {
            Console.WriteLine("FormattableString analysis");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine($"Format: {s.Format}");
            Console.WriteLine($"ArgumentCount: {s.ArgumentCount}");
            var arguments = s.GetArguments();
            for (int i = 0; i < arguments.Length; i++)
                Console.WriteLine($"Argument#{i}: {arguments[i]}");

            var input = (string)arguments[0];
            var funcObj = arguments[1];
            var func = (Func<string, int>) funcObj;
            var result = func(input);

            var formatted = string.Format(s.Format, input, result);

            Console.WriteLine($"Result: \"{formatted}\"");

        }
    }
}
