namespace HelloWorldMvc.Models.LINEPayload;

public class WebhookEvent
{
    public string? destination { get; set; }
    public IEnumerable<dynamic> events { get; set; }
}

