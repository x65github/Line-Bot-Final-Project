namespace HelloWorldMvc.Models.LINEPayload;

public class PushMessage
{
    public string? to { get; set; }
    //public IEnumerable<TextMessage> messages { get; set; }
    public IEnumerable<MessageObjectBase> messages { get; set; }
}
