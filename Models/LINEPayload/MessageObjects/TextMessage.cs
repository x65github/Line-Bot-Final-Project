namespace HelloWorldMvc.Models.LINEPayload;

/// <summary>
//// LINE Message Object: Text
/// https://developers.line.biz/en/reference/messaging-api/#text-message
/// </summary>
public class TextMessage: MessageObjectBase
{
    public string? type { get; set; } = "text";
    
    public string? text { get; set; }
}
