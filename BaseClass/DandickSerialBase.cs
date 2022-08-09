
using DKCommunicationNET. Core;

namespace DKCommunicationNET. BaseClass;

public class DandickSerialBase<TTransform> : SerialBase where TTransform : IByteTransform, new()
{
    public DandickSerialBase ( )
    {
        byteTransform = new TTransform ( );    // 实例化数据转换规则
    }
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

    /// <summary>
    /// 当前客户端的数据变换机制，当需要从字节数据转换类型数据的时候需要。
    /// </summary>
    public TTransform ByteTransform
    {
        get { return byteTransform; }
        set { byteTransform = value; }
    }
    private TTransform byteTransform;                // 数据变换的接口

    public  void HandShake ( )
    {

    }
}
