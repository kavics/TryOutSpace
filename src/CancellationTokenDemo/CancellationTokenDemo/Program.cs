using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task keyBoardTask = null;
            try
            {
                // Create a keyboard listener that can request cancel operation.
                var escapeTokenSource = new CancellationTokenSource();
                keyBoardTask = Task.Run(() => KeyboardListener(escapeTokenSource));

                var escapeToken = escapeTokenSource.Token;

                // Create a timeout requester
                var timeoutToken = new CancellationTokenSource(3000).Token;

                // Create a combined cancellation token
                var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(
                    timeoutToken, escapeToken).Token;

                // Do work with control
                string result = WorkAsync(linkedToken).Result;

                // Write result if work finished before the timeout.
                Console.WriteLine(result);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }

            if (Debugger.IsAttached)
            {
                Console.Write("press any key to exit...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static void KeyboardListener(CancellationTokenSource escapeTokenSource)
        {
            Console.WriteLine("Press <ESC> to cancel");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    escapeTokenSource.Cancel();
                    break;
                }
            }
        }

        private static Task<string> WorkAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var count = 500;
            for (var i = 0; i < count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Thread.Sleep(10);
                Console.Write("working {0}/{1}         \r", i + 1, count);
            }
            Console.WriteLine();

            return Task.FromResult("Done.");
        }
    }
}
