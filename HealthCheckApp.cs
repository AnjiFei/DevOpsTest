using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

class HealthCheckApp
{
    //private static readonly HttpClient client = new HttpClient();
     static async Task Main(string[] args)
    {
        string url = "https://this-service-url.com/healthcheck";
        string logFilePath = "healthcheck_logs.txt";

      using HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string status = response.IsSuccessStatusCode ? "UP" : "DOWN";

            string log = $"{DateTime.Now}: {url} is {status} (Status Code: {(int)response.StatusCode})";
            Console.WriteLine(log);

            await File.AppendAllTextAsync(logFilePath, log + Environment.NewLine);
        }
        catch(Exception ex) {
            string errorLog = $"{DateTime.Now}: ERROR checking{url} is - {ex.Message}";
            Console.WriteLine(errorLog);

            await File.AppendAllTextAsync(logFilePath, errorLog + Environment.NewLine);
        }
    }
}