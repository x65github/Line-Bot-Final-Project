namespace HelloWorldMvc.Models.RestApis
{
    public class ApiRequestResponse
    {
        public string apiEndPoint { get; set; }
        public string requestHeaders { get; set; }
        public string requestMethod { get; set; }
        public string requestBody { get; set; }
        public string responseStatus { get; set; }
        public string responseContent { get; set; }
    }
}