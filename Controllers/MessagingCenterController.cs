using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelloWorldMvc.Models;
using HelloWorldMvc.Models.LINEPayload;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HelloWorldMvc.MyUtilities;
using System.Diagnostics;

namespace HelloWorldMvc.Controllers;

public class MessagingCenterController : Controller
{
    // string lineChannelAccessToken = "8/ljW9rEAanIdr19+aWAjLER6kpEY+y6TbAXae1/CAwrZtvMWShsS+ZbnaWy3AdV0XUvlLBi0hgQZ2gTZyELa4wCfAgy+Yl+pqV6EtiMR3vjKbARszot/fjXgtuwMktB3pp7V+gwSAesG1Nsn8AAHAdB04t89/1O/w1cDnyilFU=";
    
    string lineChannelAccessToken = "yo9SPzaqouVCCQ4ojLHEhZr8y5VozSTleesAI2XFYiHyugaYU3a5ivu4UhZXlgrM9kC0KSWHP41kCUvw9ayyzJ/+EzVnY7dIu1uZlw2+u+OfQCCqTJBRdn3H5hMScELLd+8HGu83R0B7qbKMjeuLXAdB04t89/1O/w1cDnyilFU=";
    string chatbotName = "2022 Side Project";
    string hostUrl
    {
        get
        {
            //return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return $"https://{HttpContext.Request.Host}";
        }
    }

    LineMessagingApi lineMessagingApi;

    public MessagingCenterController()
    {

        //初始化自訂的LINE Messaging API物件
        this.lineMessagingApi = new LineMessagingApi(
            lineChannelAccessToken: this.lineChannelAccessToken
            , baseHeaders: new Dictionary<string, string>()
            , insLog: new ConsoleLog()
            );
    }

    [HttpPost]
    public async Task<IActionResult> Webhook([FromBody] WebhookEvent webhookEvent)
    {
        //Log在console
        Console.WriteLine($"Webhook : Line ChatBot");

        //查詢是否有webhook events
        if (webhookEvent.events != null)
        {
            //loop每個webhook event
            foreach (dynamic _event in webhookEvent.events)
            {
                Console.WriteLine($"input webhook event: ${_event.ToString()}");

                WebhookEventBase eventBase = JsonConvert.DeserializeObject<WebhookEventBase>(_event.ToString());
                switch (eventBase.type)
                {
                    //加入好友
                    case "follow":
                        FollowEvent followEvent = JsonConvert.DeserializeObject<FollowEvent>(_event.ToString());
                        if (followEvent != null)
                        {
                            string? replyToken = followEvent.replyToken;
                            string? userId = followEvent.source.userId;
                            string welcomeMsg = $"歡迎加入{this.chatbotName}\n本機器人尚在建置中\n目前的機器人種類：法律查詢機器人\n===使用規則===\n輸入：\n法規名稱 條號，如：民法 1\n輸出：\n法條內容，如：民事，法律所未規定者，依習慣；無習慣者，依法理。";
                            var response = await this.lineMessagingApi.welcomeMessage(replyToken,new string[] { welcomeMsg });
                        }
                        break;
                    
                    //收到訊息
                    case "message":
                        //parse coming event to Message Event
                        //解析收到的message
                        MessageEvent messageEvent = JsonConvert.DeserializeObject<MessageEvent>(_event.ToString());

                        //get user input text
                        if (messageEvent.message != null)
                        {
                            string? replyToken = messageEvent.replyToken;
                            string? userId = messageEvent.source.userId;
                            string? messageType = messageEvent.message.type;

                            IRestResponse? response = null;
                            switch (messageType)
                            {
                                case "text":
                                    string? userInputText = messageEvent.message.text;
                                    StringBuilder sb = new StringBuilder();

                                    if (userInputText.ToLower() == "help")
                                    {
                                        await this.lineMessagingApi.replyMessage(replyToken, new string[] { "===使用規則===\n 輸入：\n 法規名稱 條號，如：民法 1 \n 輸出：\n 法條內容，如：民事，法律所未規定者，依習慣；無習慣者，依法理。" });
                                    }
                                    else if (userInputText == "使用規則")
                                    {
                                        await this.lineMessagingApi.replyMessage(replyToken, new string[] { "===使用規則===\n 輸入：\n 法規名稱 條號，如：民法 1 \n 輸出：\n 法條內容，如：民事，法律所未規定者，依習慣；無習慣者，依法理。" });
                                    }
                                    else
                                    {

                                        await this.lineMessagingApi.searchlawMessage(replyToken, $"{ userInputText }" );
                                        /**                                                             
                                        response = await this.lineMessagingApi.pushMessage(
                                            to: userId,
                                            messageList: new List<MessageObjectBase>{
                                                new TextMessage(){
                                                    type = "text"
                                                    , text = $"(push message用MessageObjectBase概念發)您輸入的訊息為：{userInputText}"
                                                }
                                            }
                                        );
                                        **/
                                    }

                                    /**
                                    if (response != null)
                                    {
                                        Console.WriteLine($"send message & get status code: {response.StatusCode}, content: {response.Content}");
                                        await this.lineMessagingApi.replyMessage(replyToken, new string[] {$"此訊息格式錯誤"});
                                    }
                                    else
                                    {
                                        Console.WriteLine($"send no message");
                                    }
                                    **/
                                    break;

                                default:
                                    await this.lineMessagingApi.replyMessage(replyToken, new string[] {$"無法回應您傳過來的訊息型別：{messageType}"});
                                    break;

                            }


                        }

                        //find if the input text in any GeoLocation city name

                        //reply message

                        break;
                }
            }
        }
        return Ok();
    }

//     public async Task<string> test(string lawNumber){
// ProcessStartInfo start = new ProcessStartInfo();
//      start.FileName = "D:/lawWebCrawler.exe ";
//      //"my/full/path/to/python.exe";
//      start.Arguments = $"--law \"{lawNumber}\"";
//      start.UseShellExecute = false;
//      start.RedirectStandardOutput = true;
//      string result="";
//      using(Process process = Process.Start(start))
//      {
//          using(StreamReader reader = process.StandardOutput)
//          {
//               result = reader.ReadToEnd();
//              Console.Write(result);
//          }
//      }

//      return result;
//     }
}
