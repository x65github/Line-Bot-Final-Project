namespace HelloWorldMvc.Models.LINEPayload;

/// <summary>
/// abstract：抽象class，無法被new，是用來實現『多型』
/// 每個繼承自 class MessageObjectBase 的 class，在new後的object，均可被視為『MessageObjectBase』的物件
/// </summary>
abstract public class MessageObjectBase
{
}

