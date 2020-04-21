using System.Threading.Tasks;
using VirusScanTest.Console.VirusScanClient.Models;

namespace VirusScanTest.Console.VirusScanClient
{
    public interface IVirusScannerClient
    {
        Task<VirusScanResult> ScanAsync(byte[] file);
    }
}