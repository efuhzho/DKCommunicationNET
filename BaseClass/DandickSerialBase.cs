
using DKCommunicationNET. Core;

namespace DKCommunicationNET. BaseClass;

/// <summary>
/// 所有丹迪克串口设备的基类
/// </summary>
/// <typeparam name="TTransform">数据转换器</typeparam>
public class DandickSerialBase<TTransform> : SerialBase where TTransform : IByteTransform, new()
{
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 联机方法
    /// </summary>
    /// <returns>带成功标志的操作结果对象</returns>
    public virtual OperateResult<byte[ ]> HandShake ( )
    {
        return OperateResult. CreateSuccessResult ( Array. Empty<byte> ( ) );
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns><inheritdoc/></returns>
    protected override OperateResult InitializationOnOpen ( )
    {
        return HandShake ( );
    }
}
