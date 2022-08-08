namespace DKCommunicationNET. BaseClass;

public class DandickSerialBase : SerialBase
{
    /// <summary>
    /// 设备协议类型
    /// </summary>
    public Models Model { get; set; } 

    /// <summary>
    /// 设备ID
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// 设备出厂编号
    /// </summary>
    public string SN { get; set; } = string. Empty;

    public  void HandShake ( )
    {

    }
}
