using System;
using System.IO;
using System.Threading.Tasks;
using Cloudmersive.APIClient.NETCore.VirusScan.Api;
using Cloudmersive.APIClient.NETCore.VirusScan.Client;
using VirusScanTest.Console.VirusScanClient.Models;
using VirusScanTest.Console.Extensions;
using AppScanResult = VirusScanTest.Console.VirusScanClient.Models.VirusScanResult;

namespace VirusScanTest.Console.VirusScanClient
{
    //https://api.cloudmersive.com/net-core-client.asp
    public class CloudmersiveVirusScanner : IVirusScannerClient
    {
        private readonly IScanApi _scanApi;

        public CloudmersiveVirusScanner(string apiKey)
        {
            Configuration.Default.AddApiKey("Apikey", apiKey);
            _scanApi = new ScanApi();
        }

        public async Task<AppScanResult> ScanAsync(byte[] file)
        {
            try
            {
                var cloudmersiveResult = await _scanApi.ScanFileAsync(new MemoryStream(file));
                var noVirus = cloudmersiveResult.CleanResult ?? false;
                var result = new AppScanResult
                {
                    Status = !noVirus ? "Failed" : "Success",
                    Error = noVirus ? null : new VirusError  { Message = "Virus Found" }
                };

                return result;
            }
            catch (Exception e)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Error Calling Cloudmersive Scan File API. Error: {e}");
                System.Console.WriteLine(e.ToString());
                System.Console.ResetColor();


                return new AppScanResult
                {
                    Status = "Unhandled Error",
                    Error = new VirusError
                    {
                        Message = e.GetDeepestMessage(),
                        IsUnhandled = true
                    }
                };
            }
        }
    }
}