namespace HelloWorldMvc.Models.LINEPayload;

public class ReplyMessage
{
    public string? replyToken { get; set; }
    //public IEnumerable<TextMessage> messages { get; set; }
    public IEnumerable<MessageObjectBase> messages { get; set; }
}
