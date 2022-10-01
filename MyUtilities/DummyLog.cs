using RestSharp;
namespace HelloWorldMvc.MyUtilities;

public class DummyLog : ILog
{
    public dynamic logMsg { get; set; }

    public void log()
    {
        //do nothing
    }
}
