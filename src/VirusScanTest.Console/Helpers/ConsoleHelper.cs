using System;
using System.IO;
using System.Linq;

namespace VirusScanTest.Console.Helpers
{
    public static class ConsoleHelper
    {
        public static int GetIntInput(string prompt, bool allowNull = false, int[] allowedInts = null)
        {
            RenderPrompt(prompt, allowNull);
            var consoleInput = System.Console.ReadLine();
            int consoleOutput = 0;
            var failsValidation = true;

            while (failsValidation)
            {
                if (!int.TryParse(consoleInput, out consoleOutput) ||
                    (allowedInts != null && allowedInts.Any() && !allowedInts.Contains(consoleOutput)))
                {
                    if (allowNull)
                    {
                        failsValidation = false;
                    }
                    else
                    {
                        RenderPrompt(prompt, allowNull);
                        consoleInput = System.Console.ReadLine();
                    }
                }
                else
                {
                    failsValidation = false;
                }
            }
            return consoleOutput;
        }

        public static DateTime GetDateInput(string prompt, bool allowNull = false)
        {
            RenderPrompt(prompt, allowNull);
            var consoleInput = System.Console.ReadLine();
            DateTime consoleOutput = DateTime.MinValue;
            while (!allowNull && (string.IsNullOrWhiteSpace(consoleInput) || !DateTime.TryParse(consoleInput, out consoleOutput)))
            {
                RenderPrompt(prompt, allowNull);
                consoleInput = System.Console.ReadLine();
            }
            return consoleOutput;
        }

        public static string GetStringInput(string prompt, bool allowNull = false)
        {
            RenderPrompt(prompt, allowNull);
            var consoleInput = System.Console.ReadLine();
            string consoleOutput = string.Empty;
            while (!allowNull && string.IsNullOrWhiteSpace((consoleOutput = consoleInput)))
            {
                RenderPrompt(prompt, allowNull);
                consoleInput = System.Console.ReadLine();
            }
            return consoleOutput;
        }

        public static string GetFilePathInput(string prompt, bool allowNull = false)
        {
            RenderPrompt(prompt, allowNull);
            var consoleInput = System.Console.ReadLine();
            if (!allowNull)
            {
                while (string.IsNullOrWhiteSpace(consoleInput) || !File.Exists(consoleInput ?? ""))
                {
                    RenderPrompt(prompt);
                    consoleInput = System.Console.ReadLine();
                }
            }
            
            return consoleInput;
        }

        public static void RenderOutput(string output, int? lineBreaks = null)
        {
            if (lineBreaks.HasValue && lineBreaks > 0)
            {
                for (var i = 0; i < lineBreaks; i++)
                {
                    output = $"{output}{Environment.NewLine}";
                }
            }
            System.Console.WriteLine(output);
        }

        public static void RenderNewLine()
        {
            System.Console.WriteLine(Environment.NewLine);
        }

        private static void RenderPrompt(string prompt, bool allowNull = false)
        {
            System.Console.Write($"{prompt} [{(allowNull ? "Optional" : "Required")}]:  ");
        }
    }
}