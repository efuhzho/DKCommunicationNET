
using DKCommunicationNET. Core;

namespace DKCommunicationNET. BaseClass;

/// <summary>
/// 所有丹迪克串口设备的基类
/// </summary>
/// <typeparam name="TTransform">数据转换器</typeparam>
public class DandickSerialBase<TTransform> : SerialBase where TTransform : IByteTransform, new()
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DandickSerialBase ( )
    {
        // 实例化数据转换规则
        byteTransform = new TTransform ( );    
    }

    /// <summary>
    /// 设备ID
    /// </summary>
    public ushort ID { get; set; }

    /// <summary>
    /// 当前客户端的数据变换机制，当需要从字节数据转换类型数据的时候需要。
    /// </summary>
    public TTransform ByteTransform
    {
        get { return byteTransform; }
        set { byteTransform = value; }
    }

    // 数据变换的接口
    private TTransform byteTransform;               

    /// <summary>
    /// 联机命令：执行该命令则自动获取设备信息并初始化实例对象
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
