namespace VirusScanTest.Console.VirusScanClient.Models
{
    public class VirusScanResult
    {
        public string Status { get; set; }
        public VirusError Error { get; set; }
        public bool IsError => Error != null;
    }
}