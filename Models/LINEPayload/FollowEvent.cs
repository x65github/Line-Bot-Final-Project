namespace HelloWorldMvc.Models.LINEPayload;

public class ReceivedFollowEvent
{
    public string? destination { get; set; }
    public IEnumerable<FollowEvent> events { get; set; }
}

public class FollowEvent
{
    public string? replyToken { get; set; }
    public string? type { get; set; }
    public string? mode { get; set; }
    public double timestamp { get; set; }

    public Source? source { get; set; }
    // "source": {
    // "type": "user",
    // "userId": "U4af4980629..."
    // },
    public string? webhookEventId { get; set; }
    // "deliveryContext": {
    // "isRedelivery": false
    // }
}

public class Source
{
    public string? type { get; set; }
    public string? userId { get; set; }
}
