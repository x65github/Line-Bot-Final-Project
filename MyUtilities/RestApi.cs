using Newtonsoft.Json;
using RestSharp;
using HelloWorldMvc.Models.RestApis;
namespace HelloWorldMvc.MyUtilities;

    public class RestApi
    {
        protected Dictionary<string, string> baseHeaders;
        protected Dictionary<string, string> actionHeaders;

        ILog _insLog = null;
        protected ILog insLog
        {
            get
            {
                if (this._insLog == null)
                {
                    this._insLog = new DummyLog();
                }
                return this._insLog;
            }
            set { this._insLog = value; }
        }
        protected ApiRequestResponse logData;

        public RestApi(Dictionary<string, string> baseHeaders
            , ILog insLog = null
            )
        {
            this.baseHeaders = baseHeaders;
            this.insLog = insLog;
        }

        private async Task<IRestResponse> requestApi(string apiEndpoint, Method method, object jsonBodyPayload = null, Dictionary<string, string> formPostParameters = null)
        {
            RestClient client;
            //string WINDY_API = "https://api.windy.com/api/point-forecast/v2";
            RestRequest request;
            client = new RestClient(apiEndpoint);

            request = new RestRequest(method);
            //request.AddHeader("Content-Type", "application/json");


            this.logData = new ApiRequestResponse()
            {
                apiEndPoint = apiEndpoint,
                requestMethod = method.ToString()
            };

            Dictionary<string, string> headers = new Dictionary<string, string>();
            if (this.baseHeaders != null)
            {
                foreach (KeyValuePair<string, string> kv in this.baseHeaders)
                {
                    //request.AddHeader("Content-Type", "application/json");
                    request.AddHeader(kv.Key, kv.Value);
                    headers.Add(kv.Key, kv.Value);
                }
            }
            //bool ifFormPost = false;
            if (this.actionHeaders != null)
            {
                foreach (KeyValuePair<string, string> kv in this.actionHeaders)
                {
                    //request.AddHeader("Content-Type", "application/json");
                    request.AddHeader(kv.Key, kv.Value);
                    headers.Add(kv.Key, kv.Value);
                    // if((kv.Key.ToLower() == "Content-Type") && (kv.Value.ToLower() == "x-www-form-urlencoded")){
                    //     ifFormPost = true;
                    // }
                }

            }
            this.logData.requestHeaders = JsonConvert.SerializeObject(headers);

            if (jsonBodyPayload != null)
            {
                request.AddJsonBody(jsonBodyPayload);
                this.logData.requestBody = JsonConvert.SerializeObject(jsonBodyPayload);
            }
            if(formPostParameters != null){
                foreach (KeyValuePair<string, string> kv in formPostParameters){
                    request.AddParameter(kv.Key, kv.Value);
                }
                this.logData.requestBody = JsonConvert.SerializeObject(formPostParameters);
            }

            var cancellationTokenSource = new CancellationTokenSource();



            IRestResponse t = await client.ExecuteAsync(request, cancellationTokenSource.Token);

            this.logData.responseContent = t.Content;
            this.logData.responseStatus = t.StatusCode.ToString();

            this.logResponse(t);

            return t;
        }

        public async Task<IRestResponse> postJsonBody(string apiEndpoint, object jsonBodyPayload, Dictionary<string, string> actionHeaders = null)
        {
            this.actionHeaders = actionHeaders;
            IRestResponse t = await requestApi(apiEndpoint, Method.POST, jsonBodyPayload);
            return t;
        }
        public async Task<IRestResponse> postForm(string apiEndpoint, Dictionary<string, string> formPostParameters, Dictionary<string, string> actionHeaders = null)
        {
            this.actionHeaders = actionHeaders;
            IRestResponse t = await requestApi(apiEndpoint, Method.POST, null, formPostParameters);
            return t;
        }
        public async Task<IRestResponse> get(string apiEndpoint, Dictionary<string, string> actionHeaders = null)
        {
            this.actionHeaders = actionHeaders;
            IRestResponse t = await requestApi(apiEndpoint, Method.GET);
            return t;
        }

        virtual protected void logResponse(IRestResponse r)
        {
            this.insLog.logMsg = this.logData;
            this.insLog.log();
        }

        
    }
