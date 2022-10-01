namespace HelloWorldMvc.Models;

public class HelloUser
{
    public string? userName { get; set; }
    public bool ifFormPost { get; set; }
    public string? welcomeMessage { get; set; }
    public string? city { get; set; }
    public string? visitorType { get; set; }
    public int? timesOfLabAttended { get; set; }
}
