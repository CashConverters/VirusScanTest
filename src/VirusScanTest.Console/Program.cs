using System;
using System.IO;
using System.Text.Json;
using SystemConsole = System.Console;
using System.Threading.Tasks;
using VirusScanTest.Console.Helpers;
using VirusScanTest.Console.VirusScanClient;

namespace VirusScanTest.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                SystemConsole.ForegroundColor = ConsoleColor.Red;
                SystemConsole.WriteLine(ex.Message);
                SystemConsole.ResetColor();
            }

            SystemConsole.WriteLine("Press any key to exit");
            SystemConsole.ReadKey();
        }

        private static async Task RunAsync()
        {
            var settings = AppSettings.ReadFromJsonFile();
            IVirusScannerClient client = new CloudmersiveVirusScanner(settings.CloudmersiveApiKey);

            var keepThatBoyGoing = true;
            while (keepThatBoyGoing)
            {
                var filePath = ConsoleHelper.GetFilePathInput("Please enter path of file to scan");
                var fileBytes = await File.ReadAllBytesAsync(filePath);

                var result = await client.ScanAsync(fileBytes);
                SystemConsole.WriteLine($"Scan Result for {filePath}:");
                SystemConsole.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions{ WriteIndented = true }));
                
                keepThatBoyGoing = ConsoleHelper.GetIntInput($"Keep this boy going? 1 = Yes:", true) == 1;
            }
        }
    }
}
