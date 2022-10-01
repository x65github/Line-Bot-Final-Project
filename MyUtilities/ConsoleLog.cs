using Newtonsoft.Json;
using RestSharp;
namespace HelloWorldMvc.MyUtilities;

public class ConsoleLog : ILog
{
    public dynamic logMsg { get; set; }

    public void log()
    {
        Console.WriteLine(JsonConvert.SerializeObject(logMsg));
    }
}
