namespace HelloWorldMvc.Models.LINEPayload;


public class MessageEvent
{
    public string? replyToken { get; set; }
    public string? type { get; set; }
    public string? mode { get; set; }
    public double timestamp { get; set; }

    public MessageEventSource? source { get; set; }
    // "source": {
    // "type": "user",
    // "userId": "U4af4980629..."
    // },
    public string? webhookEventId { get; set; }
    // "deliveryContext": {
    // "isRedelivery": false
    // }

    public Message? message { get; set; }
}

public class MessageEventSource
{
    public string? type { get; set; }
    public string? userId { get; set; }
}
public class Message
{
    public string? id { get; set; }
    public string? type { get; set; }
    public string? text { get; set; }
    /*
    {
        "id": "325708",
        "type": "text",
        "text": "@example Hello, world! (love)",
        "emojis": [
            {
            "index": 23,
            "length": 6,
            "productId": "5ac1bfd5040ab15980c9b435",
            "emojiId": "001"
            }
        ],
        "mention": {
            "mentionees": [
            {
                "index": 0,
                "length": 8,
                "userId": "U850014438e..."
            }
            ]
        }
        }

    */
}
