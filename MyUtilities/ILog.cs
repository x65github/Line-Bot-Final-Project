using RestSharp;
namespace HelloWorldMvc.MyUtilities;

    public interface ILog
    {
        dynamic logMsg{ get;set;}
        void log();
    }
