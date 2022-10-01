using Newtonsoft.Json;
using System.Diagnostics;
using RestSharp;
using HelloWorldMvc.Models.LINEPayload;
using System.Diagnostics;

namespace HelloWorldMvc.MyUtilities;

    public class LineMessagingApi: RestApi
    {
        string LineChannelAccessToken {get;set;}

        public LineMessagingApi(
            string lineChannelAccessToken
            , Dictionary<string, string> baseHeaders
            , ILog insLog = null
            ): base(baseHeaders, insLog)
        {
            this.LineChannelAccessToken = lineChannelAccessToken;
            this.baseHeaders = baseHeaders;
            this.insLog = insLog;
        }

        public async Task<IRestResponse> welcomeMessage(string? replyToken, string[] messageList)
        {
            string apiEndpoint = "https://api.line.me/v2/bot/message/reply";

            ReplyMessage replyMessage = new ReplyMessage()
            {
                replyToken = replyToken
            };
            
            List<TextMessage> replyMessageList = new List<TextMessage>();
            foreach(string message in messageList){
                replyMessageList.Add(
                    new TextMessage()
                    {
                        type = "text",
                        text = message
                    }
                );
            }
            replyMessage.messages = replyMessageList;
            
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/json");
            header.Add("Authorization", $"Bearer {LineChannelAccessToken}");

            var t = await this.postJsonBody(
                apiEndpoint: apiEndpoint
                ,jsonBodyPayload: replyMessage
                , header
            );
            
            return t;
        }
        public async Task<IRestResponse> replyMessage(string? replyToken, string[] messageList)
        {
            string apiEndpoint = "https://api.line.me/v2/bot/message/reply";

            ReplyMessage replyMessage = new ReplyMessage()
            {
                replyToken = replyToken
            };
            
            List<TextMessage> replyMessageList = new List<TextMessage>();
            foreach(string message in messageList){
                replyMessageList.Add(
                    new TextMessage()
                    {
                        type = "text"
                        //, text = "歡迎加入氣象查詢機器人"
                        ,
                        text = message
                    }
                );
            }
            replyMessage.messages = replyMessageList;
            
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/json");
            header.Add("Authorization", $"Bearer {LineChannelAccessToken}");

            var t = await this.postJsonBody(
                apiEndpoint: apiEndpoint
                ,jsonBodyPayload: replyMessage
                , header
            );
            
            return t;
        }

    public async Task<string> test(string lawNumber){
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/Users/USER/Downloads/Line Chat 工作坊/project/lawWebCrawler.exe ";
        //"my/full/path/to/python.exe";
        start.Arguments = $"--law \"{lawNumber}\"";
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        string result="";
        using(Process process = Process.Start(start))
        {
            using(StreamReader reader = process.StandardOutput)
            {
                result = reader.ReadToEnd();
                Console.Write(result);
            }
        }
        return result;
    }

        public async Task<IRestResponse> searchlawMessage(string? replyToken, string? message)
        {
            string apiEndpoint = "https://api.line.me/v2/bot/message/reply";
            
            ReplyMessage replyMessage = new ReplyMessage()
            {
                replyToken = replyToken
            };
            string lawtext = await this.test(message);


            List<TextMessage> replyMessageList = new List<TextMessage>();
            replyMessageList.Add(
            new TextMessage()
            {
                type = "text"
                //, text = "歡迎加入氣象查詢機器人"
                ,
                text = "您的搜尋結果：\n"+message+"\n===\n"+lawtext
            }
            );
            replyMessage.messages = replyMessageList;
            
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/json");
            header.Add("Authorization", $"Bearer {LineChannelAccessToken}");

            var t = await this.postJsonBody(
                apiEndpoint: apiEndpoint
                ,jsonBodyPayload: replyMessage
                , header
            );
            
            return t;
        }

        public async Task<IRestResponse> pushMessage(string? to, List<MessageObjectBase> messageList)
        {
            string apiEndpoint = "https://api.line.me/v2/bot/message/push";
            
            PushMessage pushMessage = new PushMessage()
            {
                to = to, 
                messages = messageList
            };
            //welcome message
            
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/json");
            header.Add("Authorization", $"Bearer {LineChannelAccessToken}");

            var t = await this.postJsonBody(
                apiEndpoint: apiEndpoint
                ,jsonBodyPayload: pushMessage
                , header
            );
            
            return t;
        }


    }
